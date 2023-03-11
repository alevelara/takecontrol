using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Emails.Domain.Errors.Templates;

public sealed class TemplateError : DomainError
{
    public TemplateError(int codeId, string message) : base(codeId, message)
    {
    }

    public static TemplateError TemplateNotFound = new TemplateError(1601, "Template doesnt exist.");
}
