﻿using Newtonsoft.Json;
using System.Net;
using takecontrol.API.Errors;
using takecontrol.Application.Exceptions;

namespace takecontrol.API.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{    
    private readonly IHostEnvironment _environment;
    private static int NOT_FOUND_CODE_ID = 1404;
    private static int BAD_REQUEST_CODE_ID = 1400;

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
            var codeId = 0;
            var resultMessage = string.Empty;

            switch (ex) 
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    codeId = NOT_FOUND_CODE_ID;
                    break;
                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    codeId = BAD_REQUEST_CODE_ID;
                    break;
                case ValidationException:
                    var validationException = ex as ValidationException;
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var validationJson = JsonConvert.SerializeObject(validationException?.Errors);
                    resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, validationException.Error.CodeId, validationException?.Error.Message, validationJson));
                    break;
                case UnauthorizedException:
                    var unauthorizedException = ex as UnauthorizedException;
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, unauthorizedException.Error.CodeId, unauthorizedException?.Error.Message, null));
                    break;
                case ConflictException:
                    var conflictException = ex as ConflictException;
                    statusCode = (int)HttpStatusCode.Conflict;
                    resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, conflictException.Error.CodeId, conflictException?.Error.Message, null));
                    break;
            }

            context.Response.StatusCode = statusCode;

            if (string.IsNullOrEmpty(resultMessage))
                resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, codeId, ex.Message, ex.StackTrace)) ;

            await context.Response.WriteAsync(resultMessage);
        }
    }    
}
