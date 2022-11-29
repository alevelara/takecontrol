using Newtonsoft.Json;
using System.Net;
using takecontrol.API.Errors;
using takecontrol.Application.Exceptions;

namespace takecontrol.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{    
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(IHostEnvironment environment)
    {        
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {            
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var resultMessage = string.Empty;

            if (ex is NotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }

            if (ex is BadRequestException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            if (ex is ValidationException)
            {
                var validationException = ex as ValidationException;
                statusCode = (int)HttpStatusCode.BadRequest;
                var validationJson = JsonConvert.SerializeObject(validationException?.Errors);
                resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
            }

            if (ex is UnauthorizedException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                resultMessage= JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, null)); 
            }

            if (ex is ConflictException)
            {
                statusCode = (int)HttpStatusCode.Conflict;
                resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, null)); 
            }

            context.Response.StatusCode = statusCode;

            if (string.IsNullOrEmpty(resultMessage))
                resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));

            await context.Response.WriteAsync(resultMessage);
        }
    }    
}
