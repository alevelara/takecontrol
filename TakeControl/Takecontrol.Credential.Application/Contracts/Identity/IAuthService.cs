using Takecontrol.Credential.Application.Features.Accounts.Commands.ResetPassword;
using Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;
using Takecontrol.Credential.Application.Features.Accounts.Queries.Login;
using Takecontrol.Credential.Domain.Messages.Identity;

namespace Takecontrol.Credential.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginQuery request);

    Task<Guid> Register(RegistrationRequest request);

    Task<bool> ResetPassword(ResetPasswordCommand request);

    Task<bool> UpdatePassword(UpdatePasswordCommand request);
}
