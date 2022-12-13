using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.API.IntegrationTests.Controllers;

public class AuthControllerXUnitTests : TestBase
{

    public static string LOGIN_ENDPOINT = "api/v1/auth/Login";
    private HttpClient _contextHttpClient;

    public AuthControllerXUnitTests() : base()
    {
        base.DisposeIdentity();
        _contextHttpClient = RegisterUserAsAdminAsync().Result;
    }

    [Fact]
    public async Task Login_Should_ReturnOK_WhenLoginQueryIsValid()
    {
        var request = new AuthRequest
        {
            Email = "test@admin.com",
            Password = "Password123!"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new AuthRequest
        {
            Email = "invalid@email.com",
            Password = "Password123!"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnUnhathorized_WhenPasswordIsIncorrect()
    {
        var request = new AuthRequest
        {
            Email = "test@admin.com",
            Password = "incorrectPassword"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnBadRequest_WhenEmailIsEmpty()
    {
        var request = new AuthRequest
        {
            Email = "",
            Password = "incorrectPassword"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnUnhathorized_WhenPasswordIsEmpty()
    {
        var request = new AuthRequest
        {
            Email = "test@admin.com",
            Password = ""
        };

        var response = await _contextHttpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnBadRequest_WhenEmailIsWrongFormed()
    {
        var request = new AuthRequest
        {
            Email = "wrongemail",
            Password = "incorrectPassword"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
