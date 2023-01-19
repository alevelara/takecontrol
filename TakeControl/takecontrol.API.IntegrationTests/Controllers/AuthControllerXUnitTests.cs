using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.API.Routes;
using takecontrol.Domain.Messages.Identity;
using Xunit.Priority;

namespace takecontrol.API.IntegrationTests.Controllers;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "IntegrationIdentityTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(10)]
public class AuthControllerXUnitTests : IAsyncLifetime
{
    public static string MAIN_ENDPOINT = "api/v1/auth/";
    public static string LOGIN_ENDPOINT = MAIN_ENDPOINT + AuthRouteName.Login;
    public static string RESET_PASSWORD_ENDPOINT = MAIN_ENDPOINT + AuthRouteName.ResetPassword;
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

    [Fact]
    [Priority(0)]
    public async Task ResetPassword_Should_ReturnCreated_WhenRequestParamsAreValid()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new ResetPasswordRequest
        {
            Email = "test@admin.com",
            CurrentPassword = "Password123!",
            NewPassword = "Password124!",
        };

        var response = await this._httpClient.PostAsJsonAsync<ResetPasswordRequest>(RESET_PASSWORD_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task ResetPassword_Should_ReturnNotFoundException_WhenUserDoesntExist()
    {
        var request = new ResetPasswordRequest
        {
            Email = "test@admin.com",
            CurrentPassword = "Password123!",
            NewPassword = "Password124!",
        };

        var response = await this._httpClient.PostAsJsonAsync<ResetPasswordRequest>(RESET_PASSWORD_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task ResetPassword_Should_ReturnConflictException_WhenCurrentPasswordIsIncorrect()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new ResetPasswordRequest
        {
            Email = "test@admin.com",
            CurrentPassword = "Incorrect12!",
            NewPassword = "Password124!",
        };

        var response = await this._httpClient.PostAsJsonAsync<ResetPasswordRequest>(RESET_PASSWORD_ENDPOINT, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _takeControlIdentityDb.ResetState();
}