using Takecontrol.Emails.Domain.Models.Emails;

namespace Takecontrol.Emails.Application.Contracts.Emails;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(Email email, string payload, CancellationToken ct = default);
}
