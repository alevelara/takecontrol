using Takecontrol.Emails.Application.Services.Emails;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.Shared.Configuration.Abstractions.Mediatr;

namespace Takecontrol.Emails.Application.Features.Emails.Commands.SendEmails;

public sealed class SendWelcomeEmailEventNotificationHandler : IEventNotificationHandler<SendWelcomeEmailEventNotification>
{
    private readonly ISendEmailService _emailSender;

    public SendWelcomeEmailEventNotificationHandler(ISendEmailService emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(SendWelcomeEmailEventNotification notification, CancellationToken cancellationToken)
    {
        var email = Email.Create(notification.EmailTo, "Welcome to takecontrol", TemplateType.WELCOME);
        await _emailSender.SendEmailAsync(email, cancellationToken);
    }
}
