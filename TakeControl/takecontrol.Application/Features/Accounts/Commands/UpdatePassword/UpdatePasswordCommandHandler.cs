using MediatR;
using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;

namespace takecontrol.Application.Features.Accounts.Commands.UpdatePassword;

public sealed class UpdatePasswordCommandHandler : ICommandHandler<UpdatePasswordCommand, Unit>
{
    private readonly IAuthService _authService;
    //private readonly string _token;

    public UpdatePasswordCommandHandler(IAuthService authService)
    {
        _authService = authService;
      //  _token = token;
    }

    public async Task<Unit> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        await _authService.UpdatePassword(request);
        return Unit.Value;
    }
}
