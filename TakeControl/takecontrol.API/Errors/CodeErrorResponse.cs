using System.Reflection;

namespace Takecontrol.API.Errors;

public class CodeErrorResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public int CodeId { get; set; }

    public CodeErrorResponse(int statusCode, int codeId, string? message)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageStatusCode(statusCode);
        CodeId = codeId;
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "Request with errors",
            401 => "Resource not authorized",
            404 => "Resource not found",
            500 => "Internal Server Error",
            _ => string.Empty
        };
    }
}
