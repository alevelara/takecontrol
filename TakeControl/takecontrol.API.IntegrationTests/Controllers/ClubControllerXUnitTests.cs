﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.API.Routes;
using takecontrol.Domain.Messages.Clubs;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.Clubs;
using Xunit.Priority;

namespace takecontrol.API.IntegrationTests.Controllers;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(20)]
public class ClubControllerXUnitTests : IAsyncLifetime
{
    private static string mainEndpoint = "api/v1/club/";
    private static string registerEndpoint = mainEndpoint + ClubRouteName.Register;

    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly HttpClient _httpClient;

    public ClubControllerXUnitTests(CustomWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
    }

    #region Tests for registerClub Endpoint

    [Fact]
    [Priority(19)]
    public async Task RegisterClub_Should_Return201StatusCode_WhenRegisterRequestIsValid()
    {
        var request = new RegisterClubRequest
        {
            City = "CityTest",
            Email = "email@test.com",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    [Priority(21)]
    public async Task RegisterClub_Should_ReturnConflict_WhenUserWithSameEmailAlreadyExist()
    {
        await this.RegisterClubForTest();

        var request = new RegisterClubRequest
        {
            City = "CityTest",
            Email = "email@test.com",
            MainAddress = "mainAddressTest",
            Name = "nameTest2",
            Password = "Password123!",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new RegisterClubRequest
        {
            City = "CityTest",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenPasswordIsIncorrect()
    {
        var request = new RegisterClubRequest
        {
            City = "CityTest",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "pass",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenCityIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = string.Empty,
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenProvinceIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "city",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = string.Empty,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenMainAddressIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "city",
            Email = "email",
            MainAddress = string.Empty,
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenNameIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "city",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = string.Empty,
            Password = "Password123!",
            Province = "provinceTest",
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region Tests for GetByUserId Endpoint

    [Fact]
    public async Task GetByUserId_Should_ThrownClubNotFoundException_WhenUserIdDoesntExist()
    {
        //Arrange
        var userId = Guid.NewGuid().ToString();

        //Act
        var response = await this._httpClient.GetAsync(mainEndpoint + $"?userId={userId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetByUserId_Should_ReturnClub_WhenUserIdExistInDatabase()
    {
        //Arrange
        var clubName = "nameTest";
        await this.RegisterClubForTest();

        var club = await GetClubByName(clubName);

        //Act
        var response = await this._httpClient.GetAsync(mainEndpoint + $"?userId={club.UserId}");

        //Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetByUserId_Should_ThrownValidationError_WhenUserIdIsEmpty()
    {
        //Arrange
        var userId = Guid.Empty;

        //Act
        var response = await this._httpClient.GetAsync(mainEndpoint + $"?userId={userId}");

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    private async Task RegisterClubForTest()
    {
        var request = new RegisterClubRequest
        {
            City = "CityTest",
            Email = "email@test.com",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest",
        };

        await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(registerEndpoint, request, default);
    }

    private async Task<Club> GetClubByName(string name)
    {
        return await _takeControlDb.Context.Clubs?.FirstOrDefaultAsync(c => c.Name == name);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        _takeControlDb.ResetState();
        _takeControlIdentityDb.ResetState();
        _takeControlEmailDb.ResetState();
    }
}
