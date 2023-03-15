using Takecontrol.Domain.Models.Emails;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.Application.Services.Emails;

public interface ISendEmailService
{
    Task SendEmailAsync(Email email, CancellationToken cancellationToken);
}
