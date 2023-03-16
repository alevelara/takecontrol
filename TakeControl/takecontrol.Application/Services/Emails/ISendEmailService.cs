using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.Application.Services.Emails;

public interface ISendEmailService
{
    Task SendEmailAsync(Email email, CancellationToken cancellationToken);
}
