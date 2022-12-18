using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Clubs;

namespace takecontrol.API.IntegrationTests.Controllers;

public class ClubControllerXUnitTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    public static string REGISTER_ENDPOINT = "api/v1/club/Register";
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly TestBase _testBase;
    private readonly HttpClient _httpClient;

    public ClubControllerXUnitTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
        _testBase = new TestBase(factory, _httpClient);
    }

    [Fact]
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

    public void Dispose()
    {
        this._httpClient.Dispose();
        this._testBase.Dispose();
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
}
