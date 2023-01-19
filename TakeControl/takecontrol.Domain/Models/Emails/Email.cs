using takecontrol.Domain.Models.Emails.Enums;
using takecontrol.Domain.Models.Emails.ValueObjects;
using takecontrol.Domain.Models.Templates.Enum;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Emails;

public sealed class Email : BaseDomainModel
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public string EmailTo { get; private set; }

    public string Subject { get; private set; }

    public TemplateType Name { get; private set; }

    public EmailStatus Status { get; private set; }

    public Email(Guid userId, string emailTo, string subject, TemplateType name, EmailStatus status)
    {
        Id = new EmailId().Value;
        UserId = userId;
        Subject = subject;
        EmailTo = emailTo;
        Name = name;
        Status = status;
    }

    public static Email Create(Guid userId, string emailTo, string subject, TemplateType templateType)
    {
        return new Email(userId, emailTo, subject, templateType, EmailStatus.PENDING);
    }

    public void SetEmailStatus(EmailStatus status)
    {
        Status = status;
    }
}
