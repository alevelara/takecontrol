using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Clubs;
using Xunit.Priority;

namespace takecontrol.API.IntegrationTests.Controllers;

[Collection("IntegrationTests")]
[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(20)]
public class ClubControllerXUnitTests : IAsyncLifetime
{
    public static string REGISTER_ENDPOINT = "api/v1/club/Register";
    private readonly Func<Task> _resetDatabase;
    private readonly Func<Task> _initDatabase;
    private readonly HttpClient _httpClient;

    public ClubControllerXUnitTests(CustomWebApplicationFactory<Program> factory)
    {
        _resetDatabase = factory.ResetState;
        _initDatabase = factory.EnsureDatabase;
        _httpClient = factory.HttpClient;
    }

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

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

        await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);
    }

    public async Task InitializeAsync() => await _initDatabase();

    public async Task DisposeAsync() => await _resetDatabase();
}
