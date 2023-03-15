using Takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using Takecontrol.Application.Features.Accounts.Commands.UpdatePassword;
using Takecontrol.Application.Features.Accounts.Queries.Login;
using Takecontrol.Domain.Messages.Identity;

namespace Takecontrol.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginQuery request);

    Task<Guid> Register(RegistrationRequest request);

    Task<bool> ResetPassword(ResetPasswordCommand request);

    Task<bool> UpdatePassword(UpdatePasswordCommand request);
}
