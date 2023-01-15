using MediatR;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;

namespace takecontrol.Application.Features.Accounts.Commands.ResetPassword;

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
