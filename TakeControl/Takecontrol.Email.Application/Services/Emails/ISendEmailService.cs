using Takecontrol.Emails.Domain.Models.Emails;

namespace Takecontrol.Emails.Application.Services.Emails;

public interface ISendEmailService
{
    Task SendEmailAsync(Email email, object? model, CancellationToken cancellationToken);
}
