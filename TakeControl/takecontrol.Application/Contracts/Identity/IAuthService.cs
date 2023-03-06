using takecontrol.Application.Features.Accounts.Commands.ResetPassword;
using takecontrol.Application.Features.Accounts.Commands.UpdatePassword;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(LoginQuery request);

    Task<Guid> Register(RegistrationRequest request);

    Task<bool> ResetPassword(ResetPasswordCommand request);

    Task<bool> UpdatePassword(UpdatePasswordCommand request);
}
