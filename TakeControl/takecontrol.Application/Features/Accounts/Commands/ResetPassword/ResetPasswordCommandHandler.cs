using MediatR;
using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Identity;

namespace Takecontrol.Application.Features.Accounts.Commands.ResetPassword;

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
