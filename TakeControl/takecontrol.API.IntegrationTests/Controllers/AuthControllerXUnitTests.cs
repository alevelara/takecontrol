using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.API.IntegrationTests.Controllers;

public class AuthControllerXUnitTests : TestBase
{

    public static string LOGIN_ENDPOINT = "api/v1/auth/Login";

    [Fact]
    public async Task Login_ShouldWorks_WhenLoginQueryIsValid()
    {
        var (client, user) = await GetClientAsAdminAsync();

        var request = new AuthRequest
        {
            Email = "",
            Password = ""
        };

        var response = await client.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

    }
}
