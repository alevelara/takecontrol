using MediatR;
using Takecontrol.Emails.Application.Contracts.Emails;
using Takecontrol.Emails.Application.Services.Emails;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Events.Emails;

namespace Takecontrol.Emails.Application.Features.Emails.Commands.SendCancelMatchToPlayerEmail;

public sealed class SendCancelMatchToPlayerEmailCommandHandler : ICommandHandler<SendCancelMatchToPlayerEmailCommand, Unit>
{
    private readonly ISendEmailService _sendEmailService;

    public SendCancelMatchToPlayerEmailCommandHandler(ISendEmailService sendEmailService)
    {
        _sendEmailService = sendEmailService;
    }

    public async Task<Unit> Handle(SendCancelMatchToPlayerEmailCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.EmailTo, "Email cancelled succesfully", TemplateType.CANCELLED_FOR_PLAYER);
        object model = new
        {
            PlayerName = request.playerName,
            StartDate = request.match.startDate,
            EndDate = request.match.endDate,
            ClubName = request.match.clubId,
            CourtName = request.match.courtName
        };
        await _sendEmailService.SendEmailAsync(email, model, cancellationToken);

        return Unit.Value;
    }
}
