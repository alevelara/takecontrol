using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Templates;

namespace takecontrol.Application.Contracts.Emails;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(Email email, string payload, CancellationToken ct = default);
}
