﻿using System.Net;
using System.Net.Http.Json;
using Takecontrol.API.Routes;
using Takecontrol.API.Tests.Helpers;
using Takecontrol.API.Tests.Primitives;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.Shared.Tests.MockContexts;
using Xunit;
using Xunit.Priority;

namespace Takecontrol.API.Tests.Controllers;

[Collection(SharedTestCollection.Name)]
[Trait("Category", Category.APIIntegrationTests)]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(10)]
public class AuthControllerXUnitTests : IAsyncLifetime
{
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TestBase _testBase;
    private readonly HttpClient _httpClient;

    public AuthControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
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

        var request = new AuthRequest("test@admin.com", "Password123!");

        var response = await _httpClient.PostAsJsonAsync<AuthRequest>(Endpoints.Login, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new AuthRequest("invalid@email.com", "Password123!");

        var response = await _httpClient.PostAsJsonAsync<AuthRequest>(Endpoints.Login, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    [Priority(11)]
    public async Task Login_Should_ReturnUnhathorized_WhenPasswordIsIncorrect()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new AuthRequest("test@admin.com", "incorrectPassword");

        var response = await _httpClient.PostAsJsonAsync<AuthRequest>(Endpoints.Login, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnBadRequest_WhenEmailIsEmpty()
    {
        var request = new AuthRequest(string.Empty, "incorrectPassword");

        var response = await _httpClient.PostAsJsonAsync<AuthRequest>(Endpoints.Login, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnUnhathorized_WhenPasswordIsEmpty()
    {
        var request = new AuthRequest("test@admin.com", string.Empty);

        var response = await _httpClient.PostAsJsonAsync<AuthRequest>(Endpoints.Login, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_Should_ReturnBadRequest_WhenEmailIsWrongFormed()
    {
        var request = new AuthRequest("wrongemail", "incorrectPassword");

        var response = await _httpClient.PostAsJsonAsync<AuthRequest>(Endpoints.Login, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task ResetPassword_Should_ReturnCreated_WhenRequestParamsAreValid()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new ResetPasswordRequest("test@admin.com", "Password123!", "Password124!");

        var response = await _httpClient.PostAsJsonAsync<ResetPasswordRequest>(Endpoints.ResetPasword, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task ResetPassword_Should_ReturnNotFoundException_WhenUserDoesntExist()
    {
        var request = new ResetPasswordRequest("test@admin.com", "Password123!", "Password124!");

        var response = await _httpClient.PostAsJsonAsync<ResetPasswordRequest>(Endpoints.ResetPasword, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task ResetPassword_Should_ReturnConflictException_WhenCurrentPasswordIsIncorrect()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new ResetPasswordRequest("test@admin.com", "Incorrect12!", "Password124!");

        var response = await _httpClient.PostAsJsonAsync<ResetPasswordRequest>(Endpoints.ResetPasword, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task UpdatePassword_Should_ReturnCreated_WhenRequestParamsAreValid()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new UpdatePasswordRequest("test@admin.com", "Password124!");

        var response = await _httpClient.PostAsJsonAsync<UpdatePasswordRequest>(Endpoints.UpdatePasword, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task UpdatePassword_Should_ReturnNotFoundException_WhenUserDoesntExist()
    {
        var request = new UpdatePasswordRequest("test@admin.com", "Password124!");

        var response = await _httpClient.PostAsJsonAsync<UpdatePasswordRequest>(Endpoints.UpdatePasword, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    [Priority(0)]
    public async Task UpdatePassword_Should_ReturnConflictException_WhenCurrentPasswordIsIncorrect()
    {
        await _testBase.RegisterUserAsAdminAsync();

        var request = new UpdatePasswordRequest("test@admin.com", "Password124");

        var response = await _httpClient.PostAsJsonAsync<UpdatePasswordRequest>(Endpoints.UpdatePasword, request, CancellationToken.None);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync() => await _takeControlIdentityDb.ResetState();
}