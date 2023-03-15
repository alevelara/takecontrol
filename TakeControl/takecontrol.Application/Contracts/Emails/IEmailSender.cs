using Takecontrol.Domain.Models.Emails;
using Takecontrol.Domain.Models.Templates;

namespace Takecontrol.Application.Contracts.Emails;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(Email email, string payload, CancellationToken ct = default);
}
