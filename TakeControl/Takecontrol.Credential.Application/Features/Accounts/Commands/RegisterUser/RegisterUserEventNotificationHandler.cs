using MediatR;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Shared.Application.Events.Credentials;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.RegisterUser;

internal sealed class RegisterUserEventNotificationHandler :
    INotificationHandler<RegisterClubMessageNotification>,
    INotificationHandler<RegisterPlayerMessageNotification>
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public RegisterUserEventNotificationHandler(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    public async Task Handle(RegisterClubMessageNotification notification, CancellationToken cancellationToken)
    {
        var registerRequest = new RegistrationRequest(notification.Name, notification.Email, notification.Password, UserType.Club);
        var userId = await _authService.Register(registerRequest);

        await _mediator.Publish(new UserRegisteredEventNotification(userId));
    }

    public async Task Handle(RegisterPlayerMessageNotification notification, CancellationToken cancellationToken)
    {
        var registerRequest = new RegistrationRequest(notification.Name, notification.Email, notification.Password, UserType.Player);
        var userId = await _authService.Register(registerRequest);

        await _mediator.Publish(new UserRegisteredEventNotification(userId));
    }
}
