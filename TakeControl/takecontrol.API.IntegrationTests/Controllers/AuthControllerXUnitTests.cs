using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Identity;
using Xunit.Priority;

namespace takecontrol.API.IntegrationTests.Controllers;

[Trait("Category", "IntegrationIdentityTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(10)]
[Collection(SharedTestCollection.Name)]
public class AuthControllerXUnitTests : IAsyncLifetime
{
    public static string LOGIN_ENDPOINT = "api/v1/auth/Login";
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TestBase _testBase;
    private readonly HttpClient _httpClient;

    public AuthControllerXUnitTests(CustomWebApplicationFactory<Program> factory)
    {
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _testBase = new TestBase(factory);
    }

    [Fact]
    [Priority(0)]
    public async Task Login_Should_ReturnOK_WhenLoginQueryIsValid()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new AuthRequest
        {
            Email = "test@admin.com",
            Password = "Password123!",
        };

        var response = await this._httpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new AuthRequest
        {
            Email = "invalid@email.com",
            Password = "Password123!",
        };

        var response = await this._httpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    [Priority(11)]
    public async Task Login_Should_ReturnUnhathorized_WhenPasswordIsIncorrect()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new AuthRequest
        {
            Email = "test@admin.com",
            Password = "incorrectPassword",
        };

        var response = await this._httpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnBadRequest_WhenEmailIsEmpty()
    {
        var request = new AuthRequest
        {
            Email = string.Empty,
            Password = "incorrectPassword",
        };

        var response = await this._httpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnUnhathorized_WhenPasswordIsEmpty()
    {
        var request = new AuthRequest
        {
            Email = "test@admin.com",
            Password = string.Empty,
        };

        var response = await this._httpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnBadRequest_WhenEmailIsWrongFormed()
    {
        var request = new AuthRequest
        {
            Email = "wrongemail",
            Password = "incorrectPassword",
        };

        var response = await this._httpClient.PostAsJsonAsync<AuthRequest>(LOGIN_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _takeControlIdentityDb.ResetState();
}