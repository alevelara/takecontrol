using MediatR;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Events.Credentials;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.RegisterUser;

internal sealed class RegisterUserEventNotificationHandler :
    ICommandHandler<RegisterClubMessageNotification, Guid>,
    ICommandHandler<RegisterPlayerMessageNotification, Guid>
{
    private readonly IAuthService _authService;

    public RegisterUserEventNotificationHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Guid> Handle(RegisterClubMessageNotification request, CancellationToken cancellationToken)
    {
        var registerRequest = new RegistrationRequest(request.Name, request.Email, request.Password, UserType.Club);
        return await _authService.Register(registerRequest);
    }

    public async Task<Guid> Handle(RegisterPlayerMessageNotification request, CancellationToken cancellationToken)
    {
        var registerRequest = new RegistrationRequest(request.Name, request.Email, request.Password, UserType.Player);
        return await _authService.Register(registerRequest);
    }
}
