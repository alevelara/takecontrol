using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Takecontrol.API.Tests.Helpers;
using Takecontrol.API.Tests.Primitives;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.User.Domain.Messages.Clubs.Requests;
using Takecontrol.User.Domain.Models.Clubs;
using Xunit;
using Xunit.Priority;

namespace Takecontrol.API.Tests.Controllers;

[Collection(SharedTestCollection.Name)]
[Trait("Category", Category.APIIntegrationTests)]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(20)]
public class ClubControllerXUnitTests : IAsyncLifetime
{
    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly TakeControlMatchesDb _takeControlMatchesDb;
    private readonly HttpClient _httpClient;
    private readonly TestBase _testBase;
    private const string MainEndpoint = "api/v1/club";

    public ClubControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _takeControlMatchesDb = factory.TakeControlMatchesDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
        _testBase = new TestBase(factory);
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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);

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
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForClubs(_testBase);

        //Act
        var response = await httpClient.GetAsync(MainEndpoint + $"?userId={userId}");

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetByUserId_Should_ReturnClub_WhenUserIdExistInDatabase()
    {
        //Arrange
        var clubName = "nameTest";
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForClubs(_testBase);

        await RegisterClubForTest();
        var club = await GetClubByNameAsync(clubName);

        //Act
        var response = await httpClient.GetAsync(MainEndpoint + $"?userId={club!.UserId}");

        //Assert
        Assert.NotNull(response);
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetByUserId_Should_ThrownValidationError_WhenUserIdIsEmpty()
    {
        //Arrange
        var userId = Guid.Empty;
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForClubs(_testBase);

        //Act
        var response = await httpClient.GetAsync(MainEndpoint + $"?userId={userId}");

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    #endregion

    #region GetAllClubs Tests

    [Fact]
    public async Task GetAllClubs_Should_ReturnSuccesfulHttpStatus_WhenClubsAreInDatabase()
    {
        //Arrange
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.GetAsync(Endpoints.AllClubs);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    #endregion

    #region CancelForcedMatch Tests

    [Fact]
    public async Task Should_fail_when_club_does_not_exist()
    {
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForClubs(_testBase);
        var request = new CancelForcedMatchRequest(Guid.NewGuid(), Guid.NewGuid(), "description");
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelForcedMatch, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_match_does_not_exist()
    {
        await RegisterClubForTest();
        var club = await GetClubByNameAsync("nameTest");

        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForClubs(_testBase);
        var request = new CancelForcedMatchRequest(club!.UserId, Guid.NewGuid(), "description");
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelForcedMatch, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_cancel_match_succesfully()
    {
        await RegisterClubForTest();
        var club = await GetClubByNameAsync("nameTest");
        var reservation = await GetReservationByClubId(club!.Id);
        var match = await AddMatchForTest(reservation!.Id, Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForClubs(_testBase);

        var request = new CancelForcedMatchRequest(club!.UserId, match.Id, "description");
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelForcedMatch, request, default);

        Assert.NotNull(response);
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

        await _httpClient.PostAsJsonAsync(Endpoints.RegisterClub, request, default);
    }

    private async Task<Club?> GetClubByNameAsync(string name)
    {
        return await _takeControlDb.Context.Set<Club>().FirstOrDefaultAsync(x => x.Name == name);
    }

    private async Task<Match> AddMatchForTest(Guid reservationId, Guid playerId)
    {
        var match = Match.Create(reservationId, playerId);
        await _takeControlMatchesDb.Context.Set<Match>().AddAsync(match);
        await _takeControlMatchesDb.Context.SaveChangesAsync();
        return match;
    }

    private async Task<Reservation?> GetReservationByClubId(Guid clubId)
    {
        var court = await _takeControlMatchesDb
             .Context.Set<Court>()
             .Include(c => c.Reservations)
             .FirstOrDefaultAsync(C => C.ClubId == clubId);

        return court!.Reservations.FirstOrDefault();
    }

    #endregion

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlDb.ResetState();
        await _takeControlIdentityDb.ResetState();
        await _takeControlMatchesDb.ResetState();
        await _takeControlEmailDb.ResetState();
    }
}