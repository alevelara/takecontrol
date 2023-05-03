using Takecontrol.Emails.Domain.Models.Emails.Enums;
using Takecontrol.Emails.Domain.Models.Emails.ValueObjects;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Emails.Domain.Models.Emails;

public sealed class Email : BaseDomainModel
{
    public Guid Id { get; private set; }

    public string EmailTo { get; private set; }

    public string Subject { get; private set; }

    public TemplateType TemplateType { get; private set; }

    public EmailStatus Status { get; private set; }

    public Email(string emailTo, string subject, TemplateType templateType, EmailStatus status)
    {
        Id = new EmailId().Value;
        Subject = subject;
        EmailTo = emailTo;
        TemplateType = templateType;
        Status = status;
    }

    public static Email Create(string emailTo, string subject, TemplateType templateType)
    {
        return new Email(emailTo, subject, templateType, EmailStatus.PENDING);
    }

    public void SetEmailStatus(EmailStatus status)
    {
        Status = status;
    }
}
