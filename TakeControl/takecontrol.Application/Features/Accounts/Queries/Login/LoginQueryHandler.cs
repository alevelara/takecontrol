using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Domain.Mappings.Identity;

namespace takecontrol.Application.Features.Accounts.Queries.Login;

internal class LoginQueryHandler : IQueryHandler<LoginQuery, AuthResponse>
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
