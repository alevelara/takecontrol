
using System.Net.Http.Json;
using Takecontrol.Console.Repositories.Credentials;

namespace Takecontrol.Console.Tests.Repositories.Credentials;

[Trait("Category", "Integration")]
public class CredentialsRepositoryTests
{
    private readonly HttpClient _httpClient;

    public CredentialsRepositoryTests()
    {
        _httpClient = new HttpClient()
        {
             BaseAddress = new Uri("https://localhost:7167/"),
        };
    }

    [Fact]
    public async Task Should_login_succesfull_when_a_customer_was_previously_registered()
    {
        var repository = new CredentialsRepository(_httpClient);
        var username = "alevelara@gmail.com";
        var password = "Password123!";

        var result = await repository.Login(username, password);
        var response = result.Content.ReadFromJsonAsync<object>();
        Assert.NotNull(response);
        Assert.NotNull(result);
        Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task Should_login_fail_when_a_customer_try_invalid_credentials()
    {
        var repository = new CredentialsRepository(_httpClient);
        var username = "invalid@gmail.com";
        var password = "badpassword!";

        var result = await repository.Login(username, password);

        Assert.NotNull(result);
        Assert.Equal(System.Net.HttpStatusCode.Conflict, result.StatusCode);
    }
}
