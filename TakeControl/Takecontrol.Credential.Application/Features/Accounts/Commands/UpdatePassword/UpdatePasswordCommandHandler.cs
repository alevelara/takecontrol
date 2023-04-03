using MediatR;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;

public sealed class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand, Unit>
{
    private readonly IAuthService _authService;

    public UpdatePasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        await _authService.UpdatePassword(request);
        return Unit.Value;
    }
}
