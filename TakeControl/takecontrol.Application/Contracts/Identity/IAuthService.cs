using takecontrol.Domain.Mappings.Identity;

namespace takecontrol.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
}
