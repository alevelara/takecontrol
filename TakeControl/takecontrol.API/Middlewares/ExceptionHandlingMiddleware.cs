using Newtonsoft.Json;
using System;
using takecontrol.Application.Contracts.Logger;
using takecontrol.Application.Exceptions;

namespace takecontrol.API.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILog _logger;    
    private readonly IHostEnvironment _environment;

    public ExceptionHandlingMiddleware(ILog logger, IHostEnvironment environment)
    {
        _logger = logger;        
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
            _logger.Error(ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = GetStatusCode(ex);
        var response = new
        {
            title = GetTitle(ex),
            status = statusCode,
            detail = ex.Message,
            errors = GetErrors(ex)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        
        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));        
    }

    private static IDictionary<string, string[]>? GetErrors(Exception ex)
    {
        IDictionary<string, string[]> errors = null;
        if (ex is ValidationException validationException)
        {
            errors = validationException.Errors;
        }
        return errors;
    }

    private static string GetTitle(Exception ex) =>
        ex switch
        {
            ApplicationException applicationException => nameof(ApplicationException),
            _ => "Server Error"
        };
    
    private static int GetStatusCode(Exception ex) =>
        ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };
}
