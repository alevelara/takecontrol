namespace takecontrol.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Details { get; set; }

        public CodeErrorException(int statusCode, int codeId, string? message = null, string? details = null) : base(statusCode, codeId, message)
        {
            Details = details;
        }
    }
}
