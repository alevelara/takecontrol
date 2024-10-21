using Takecontrol.Console.Domain.Credentials;

namespace Takecontrol.Console.Services.Credentials;

public interface ICredentialService
{
    Task<AuthResponse?> Login(string username, string password);
    Task<bool> ResetPassword(string username, string userPassword, string newPassword);
    Task<bool> UpdatePassword(string username, string password, string token);
}
