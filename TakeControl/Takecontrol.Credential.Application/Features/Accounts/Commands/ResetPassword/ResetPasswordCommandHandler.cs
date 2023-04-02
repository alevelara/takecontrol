using MediatR;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Credential.Application.Features.Accounts.Commands.ResetPassword;

public sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, Unit>
{
    private readonly IAuthService _authService;

    public ResetPasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        await _authService.ResetPassword(request);
        return Unit.Value;
    }
}
