using System.Net;
using System.Net.Http.Json;
using Takecontrol.API.Routes;
using Takecontrol.API.Tests.Primitives;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.Shared.Tests.Repositories.Clubs;
using Takecontrol.User.Domain.Messages.Clubs.Requests;
using Xunit;
using Xunit.Priority;

namespace Takecontrol.API.Tests.Controllers;

[Collection(SharedTestCollection.Name)]
[Trait("Category", Category.APIIntegrationTests)]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(20)]
public class ClubControllerXUnitTests : IAsyncLifetime
{
    private static string mainEndpoint = "api/v1/club/";
    private static string registerEndpoint = mainEndpoint + ClubRouteName.Register;
    private static string allClubsEndpoint = mainEndpoint + ClubRouteName.All;

    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly HttpClient _httpClient;
    private readonly TestBase _testBase;
    private readonly TestClubReadRepository _clubReadRepository;

    public ClubControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
        _testBase = new TestBase(factory);
        _clubReadRepository = new TestClubReadRepository(_takeControlDb);
    }

    #region RegisterClub Tests

    [Fact]
    [Priority(19)]
    public async Task RegisterClub_Should_Return201StatusCode_WhenRegisterRequestIsValid()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: "nameTest",
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    [Priority(21)]
    public async Task RegisterClub_Should_ReturnConflict_WhenUserWithSameEmailAlreadyExist()
    {
        await RegisterClubForTest();

        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: "nameTest2",
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email",
            MainAddress: "mainAddressTest",
            Name: "nameTest",
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00")
            );

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenPasswordIsIncorrect()
    {
        var request = new RegisterClubRequest(
           City: "CityTest",
           Email: "email@test.com",
           MainAddress: "mainAddressTest",
           Name: "nameTest",
           Password: "pass",
           Province: "provinceTest",
           NumberOfCourts: 1,
           OpenDate: TimeOnly.Parse("10:00"),
           ClosureDate: TimeOnly.Parse("12:00")
           );

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenCityIsEmpty()
    {
        var request = new RegisterClubRequest(
            City: string.Empty,
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: "nameTest",
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenProvinceIsEmpty()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: "nameTest",
            Password: "Password123!",
            Province: string.Empty,
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenMainAddressIsEmpty()
    {
        var request = new RegisterClubRequest(
             City: "CityTest",
             Email: "email@test.com",
             MainAddress: string.Empty,
             Name: "nameTest",
             Password: "Password123!",
             Province: "provinceTest",
             NumberOfCourts: 1,
             OpenDate: TimeOnly.Parse("10:00"),
             ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenNameIsEmpty()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: string.Empty,
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenOpenDateIsMinValue()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: string.Empty,
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.MinValue,
            ClosureDate: TimeOnly.Parse("12:00"));

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterClub_Should_ReturnConflict_WhenClosureDateIsMinValue()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: string.Empty,
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.MinValue);

        var response = await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    #endregion

    #region GetByUserId Tests

    [Fact]
    public async Task GetByUserId_Should_ThrownClubNotFoundException_WhenUserIdDoesntExist()
    {
        //Arrange
        var userId = Guid.NewGuid().ToString();
        var httpClient = await AddJWTTokenToHeaderForClubs();

        //Act
        var response = await httpClient.GetAsync(mainEndpoint + $"?userId={userId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetByUserId_Should_ReturnClub_WhenUserIdExistInDatabase()
    {
        //Arrange
        var clubName = "nameTest";
        var httpClient = await AddJWTTokenToHeaderForClubs();

        await RegisterClubForTest();
        var club = await _clubReadRepository.GetClubByName(clubName);

        //Act
        var response = await httpClient.GetAsync(mainEndpoint + $"?userId={club!.UserId}");

        //Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetByUserId_Should_ThrownValidationError_WhenUserIdIsEmpty()
    {
        //Arrange
        var userId = Guid.Empty;
        var httpClient = await AddJWTTokenToHeaderForClubs();

        //Act
        var response = await httpClient.GetAsync(mainEndpoint + $"?userId={userId}");

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    #endregion

    #region GetAllClubs Tests

    [Fact]
    public async Task GetAllClubs_Should_ReturnSuccesfulHttpStatus_WhenClubsAreInDatabase()
    {
        //Arrange
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        //Act
        var response = await httpClient.GetAsync(allClubsEndpoint);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    #region Private methods

    private async Task RegisterClubForTest()
    {
        var request = new RegisterClubRequest(
            City: "CityTest",
            Email: "email@test.com",
            MainAddress: "mainAddressTest",
            Name: "nameTest",
            Password: "Password123!",
            Province: "provinceTest",
            NumberOfCourts: 1,
            OpenDate: TimeOnly.Parse("10:00"),
            ClosureDate: TimeOnly.Parse("12:00"));

        await _httpClient.PostAsJsonAsync(registerEndpoint, request, default);
    }

    private async Task<HttpClient> AddJWTTokenToHeaderForClubs()
    {
        return await _testBase.RegisterSecuredUserAsClubAsync();
    }

    private async Task<HttpClient> AddJWTTokenToHeaderForPlayers()
    {
        return await _testBase.RegisterSecuredUserAsPlayerAsync();
    }

    #endregion

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlDb.ResetState();
        await _takeControlIdentityDb.ResetState();
        await _takeControlEmailDb.ResetState();
    }
}