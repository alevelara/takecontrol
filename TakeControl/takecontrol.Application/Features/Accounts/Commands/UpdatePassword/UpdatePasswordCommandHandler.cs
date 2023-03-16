using MediatR;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;

namespace takecontrol.Application.Features.Accounts.Commands.UpdatePassword;

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
