using MediatR;
using Takecontrol.Emails.Application.Services.Emails;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Events.Emails;

namespace Takecontrol.Emails.Application.Features.Emails.Commands.SendEmails;

public sealed class SendWelcomeEmailCommandHandler : ICommandHandler<SendWelcomeEmailMessageNotification, Unit>
{
    private readonly ISendEmailService _emailSender;

    public SendWelcomeEmailCommandHandler(ISendEmailService emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task<Unit> Handle(SendWelcomeEmailMessageNotification request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.EmailTo, "Welcome to Takecontrol", TemplateType.WELCOME);
        await _emailSender.SendEmailAsync(email, cancellationToken);

        return Unit.Value;
    }
}
