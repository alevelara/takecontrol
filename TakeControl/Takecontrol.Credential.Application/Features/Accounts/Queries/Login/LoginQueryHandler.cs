using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Credential.Application.Features.Accounts.Queries.Login;

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
