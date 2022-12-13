using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Clubs;

namespace takecontrol.API.IntegrationTests.Controllers;

public class ClubControllerXUnitTests : TestBase
{
    public static string REGISTER_ENDPOINT = "api/v1/club/Register";
    private HttpClient _contextHttpClient;

    public ClubControllerXUnitTests() : base()
    {
        _contextHttpClient = base.HttpClient;
        base.Dispose();
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
            Province = "provinceTest"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
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
            Province = "provinceTest"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

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
            Province = "provinceTest"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenCityIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenProvinceIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "nameTest",
            Password = "Password123!",
            Province = ""
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenMainAddressIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "",
            Email = "email",
            MainAddress = "",
            Name = "nameTest",
            Password = "Password123!",
            Province = "provinceTest"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenNameIsEmpty()
    {
        var request = new RegisterClubRequest
        {
            City = "",
            Email = "email",
            MainAddress = "mainAddressTest",
            Name = "",
            Password = "Password123!",
            Province = "provinceTest"
        };

        var response = await _contextHttpClient.PostAsJsonAsync<RegisterClubRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}
