using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Identity;
using Takecontrol.Domain.Messages.Identity;

namespace Takecontrol.Application.Features.Accounts.Queries.Login;

public sealed class LoginQueryHandler : IQueryHandler<LoginQuery, AuthResponse>
{
    private readonly IAuthService _authService;

    public LoginQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        return await _authService.Login(request);
    }
}
