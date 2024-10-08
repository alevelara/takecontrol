using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Takecontrol.Console.Repositories.Credentials;

public class CredentialsRepository
{
    private readonly HttpClient _httpClient;

    public CredentialsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7167");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<HttpResponseMessage> Login(string username, string password)
    {
        var url = "api/v1/Auth/login";
        var jsonContent = new StringContent(
        JsonSerializer.Serialize(new
        {
            email = username,
            password = password
        }),
        Encoding.UTF8,
        Application.Json);

        return await _httpClient.PostAsync(url, jsonContent);
    }

    public async Task<HttpResponseMessage> ResetPassword(string username, string userPassword, string newPassword)
    {
        var url = "/api/v1/Auth/ResetPassword";

        using StringContent jsonContent = new(
        JsonSerializer.Serialize(new
        {
            Email = username,
            CurrentPassword = userPassword,
            NewPassword = newPassword
        }),
        Encoding.UTF8,
        "application/json");

        return await _httpClient.PostAsync(url, jsonContent);
    }

    public async Task<HttpResponseMessage> UpdatePassword(string username, string newPassword)
    {
        var url = "/api/v1/Auth/UpdatePassword";

        using StringContent jsonContent = new(
        JsonSerializer.Serialize(new
        {
            Email = username,
            NewPassword = newPassword
        }),
        Encoding.UTF8,
        "application/json");

        return await _httpClient.PostAsync(url, jsonContent);
    }
}
