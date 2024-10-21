using System.Net.Http.Json;
using Takecontrol.Console.Domain.Credentials;
using Takecontrol.Console.Repositories.Credentials;

namespace Takecontrol.Console.Services.Credentials;

public class CredentialService : ICredentialService
{
    private readonly CredentialsRepository _credentialsRepository;
    public CredentialService(CredentialsRepository credentialsRepository)
    {
        _credentialsRepository = credentialsRepository;
    }

    public async Task<AuthResponse?> Login(string username, string password)
    {

        var result = await _credentialsRepository.Login(username, password);

        if (!result.IsSuccessStatusCode)
        {
           return null;
        }

        return await result?.Content?.ReadFromJsonAsync<AuthResponse>()!;
    }

    public async Task<bool> ResetPassword(string username, string userPassword, string newPassword)
    {
        var result = await _credentialsRepository.ResetPassword(username, userPassword, newPassword);
        return result.IsSuccessStatusCode;
    }

    public async Task<bool> UpdatePassword(string username, string password, string token)
    {
        var result = await _credentialsRepository.UpdatePassword(username, password, token);
        return result.IsSuccessStatusCode;
    }
}
