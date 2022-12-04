using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginQuery request);
}
