using System.Net;
using System.Net.Http.Json;
using Takecontrol.API.Tests.Controllers.TestData;
using Takecontrol.API.Tests.Helpers;
using Takecontrol.API.Tests.Primitives;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Matches.Domain.Models.Reservations.ValueObjects;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.Shared.Tests.Repositories.Clubs;
using Takecontrol.Shared.Tests.Repositories.Courts;
using Takecontrol.Shared.Tests.Repositories.Matches;
using Takecontrol.Shared.Tests.Repositories.Players;
using Takecontrol.Shared.Tests.Repositories.Reservations;
using Takecontrol.User.Domain.Messages.Clubs.Requests;
using Takecontrol.User.Domain.Messages.Players.Requests;
using Takecontrol.User.Domain.Models.Players.Enums;
using Xunit;
using Xunit.Priority;
using Player = Takecontrol.User.Domain.Models.Players.Player;

namespace Takecontrol.API.Tests.Controllers;

[Trait("Category", Category.APIIntegrationTests)]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(30)]
[Collection(SharedTestCollection.Name)]
public class PlayerControllerXUnitTests : IAsyncLifetime
{
    private const string RegisterPlayerEndpoint = "api/v1/player/Register";
    private const string JoinToClubEndpoint = "api/v1/player/Join";
    private const string JoinToMatchEndpoint = "api/v1/player/JoinToMatch";
    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly TakeControlMatchesDb _takeControlMatchesDb;
    private readonly HttpClient _httpClient;
    private readonly TestBase _testBase;
    private readonly TestClubReadRepository _clubReadRepository;
    private readonly TestPlayerReadRepository _playerReadRepository;
    private readonly TestMatchWriteRepository _matchWriteRepository;
    private readonly TestCourtReadRepository _courtReadRepository;
    private readonly TestReservationReadRepository _reservationReadRepository;

    public PlayerControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
        _takeControlMatchesDb = factory.TakeControlMatchesDb;
        _testBase = new TestBase(factory);
        _clubReadRepository = new TestClubReadRepository(_takeControlDb);
        _playerReadRepository = new TestPlayerReadRepository(_takeControlDb);
        _matchWriteRepository = new TestMatchWriteRepository(_takeControlMatchesDb);
        _reservationReadRepository = new TestReservationReadRepository(_takeControlMatchesDb);
        _courtReadRepository = new TestCourtReadRepository(_takeControlMatchesDb);
    }

    #region RegisterPlayer Tests
    [Fact]
    [Priority(29)]
    public async Task RegisterPlayer_Should_Return201StatusCode_WhenRegisterRequestIsValid()
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_ShouldReturnCorrectLevel_WhenRegisterRequestIsValid()
    {
        var names = new List<string>();

        foreach (PlayerLevel level in Enum.GetValues(typeof(PlayerLevel)))
        {
            var request = new RegisterPlayerRequest(
                Email: $"email2{(int)level}@test.com",
                Name: $"nameTest2-{(int)level}",
                Password: "Password123!",
                AvgNumberOfMatchesInAWeek: 2,
                NumberOfClassesInAWeek: 1,
                NumberOfYearsPlayed: (int)level * (int)level);

            var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);
            Assert.True(response.IsSuccessStatusCode);

            names.Add(request.Name);
        }

        List<Player> players = _takeControlDb.Context.Players.ToList();

        Assert.Equal(names.Count, players.Count);

        foreach (var name in names)
        {
            var level = int.Parse(name.Split('-')[1]);

            var player = players!.FirstOrDefault(c => c.Name == name);
            Assert.Equal(player!.PlayerLevel, level);
        }
    }

    [Fact]
    [Priority(39)]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenUserWithSameEmailAlreadyExist()
    {
        await PlayerTestData.RegisterPlayerForTest(_httpClient);

        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new RegisterPlayerRequest(
            Email: "email.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenPasswordIsIncorrect()
    {
        var request = new RegisterPlayerRequest(
             Email: "email2@test.com",
             Name: "nameTest",
             Password: "Password!",
             AvgNumberOfMatchesInAWeek: 1,
             NumberOfClassesInAWeek: 1,
             NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenAvgNumberOfMatchesInAWeekIsNegative()
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: -1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenNumberOfClassesInAWeekIsNegative()
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: -1,
            NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenNumberOfYearsPlayedIsNegative()
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: "nameTest",
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: -1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenNameIsEmpty()
    {
        var request = new RegisterPlayerRequest(
            Email: "email2@test.com",
            Name: string.Empty,
            Password: "Password123!",
            AvgNumberOfMatchesInAWeek: 1,
            NumberOfClassesInAWeek: 1,
            NumberOfYearsPlayed: 1);

        var response = await _httpClient.PostAsJsonAsync(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region JoinToClub Tests
    [Fact]
    public async Task JoinToClub_Should_ReturnCreatedStatusCode_WhenAPlayerJoinToANewClub()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        await ClubTestData.RegisterClubForTest(_httpClient);

        var club = await _clubReadRepository.GetClubByName("nameTest");
        var player = await _playerReadRepository.GetPlayerByName("nameTest");

        var request = new JoinToClubRequest(player!.Id, club!.Id, club.Code);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToClubEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task JoinToClub_Should_ReturnConflictStatusCode_WhenAClubDoesntExist()
    {
        //Arrange
        var code = "12345";
        var request = new JoinToClubRequest(Guid.NewGuid(), Guid.NewGuid(), code);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToClubEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task JoinToClub_Should_ReturnConflictStatusCode_WhenACodeDoesntMatchWithTheClub()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        await ClubTestData.RegisterClubForTest(_httpClient);

        var club = await _clubReadRepository.GetClubByName("nameTest");
        var player = await _playerReadRepository.GetPlayerByName("nameTest");
        var incorrectCode = "12345";
        var request = new JoinToClubRequest(player!.Id, club!.Id, incorrectCode);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToClubEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
    #endregion

    #region JoinToMatch Tests
    [Fact]
    public async Task Should_Join_The_Player_To_The_Match_When_Match_Is_Open_And_Player_Is_Not_Registered_Yet()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await _clubReadRepository.GetClubByName("nameTest");
        var court = await _courtReadRepository.GetCourtByClubAsync(club!.Id);
        var reservation = await _reservationReadRepository.GetReservationByCourtAsync(court!.Id);
        var player = await _playerReadRepository.GetPlayerByName("nameTest");
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);

        var request = new JoinToAMatchRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToMatchEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_User_Is_Not_Registered()
    {
        //Arrange
        var request = new JoinToAMatchRequest(Guid.NewGuid(), Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToMatchEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_Match_Is_Not_Found()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await _playerReadRepository.GetPlayerByName("nameTest");

        var request = new JoinToAMatchRequest(player!.UserId, Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToMatchEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_Match_Is_Closed()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await _playerReadRepository.GetPlayerByName("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await _clubReadRepository.GetClubByName("nameTest");
        var court = await _courtReadRepository.GetCourtByClubAsync(club!.Id);
        var reservation = await _reservationReadRepository.GetReservationByCourtAsync(court!.Id);
        var match = await AddClosedMatchForTest(reservation!.Id, player!.UserId);

        var request = new JoinToAMatchRequest(player!.UserId, match!.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(JoinToMatchEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_Player_Was_Previously_Joint()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await _playerReadRepository.GetPlayerByName("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await _clubReadRepository.GetClubByName("nameTest");
        var court = await _courtReadRepository.GetCourtByClubAsync(club!.Id);
        var reservation = await _reservationReadRepository.GetReservationByCourtAsync(court!.Id);
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);

        var request = new JoinToAMatchRequest(player!.UserId, match!.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        await httpClient.PostAsJsonAsync(JoinToMatchEndpoint, request, default);
        var response = await httpClient.PostAsJsonAsync(JoinToMatchEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
    #endregion

    #region Private methods

    private async Task<Match> AddMatchForTest(Guid reservationId, Guid playerId)
    {
        var match = Match.Create(reservationId, playerId);
        await _matchWriteRepository.AddMatchAsync(match);
        return match;
    }

    private async Task<Match> AddClosedMatchForTest(Guid reservationId, Guid playerId)
    {
        var match = Match.Create(reservationId, playerId);
        match.Close();
        await _matchWriteRepository.AddMatchAsync(match);
        return match;
    }

    #endregion

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlIdentityDb.ResetState();
        await _takeControlDb.ResetState();
        await _takeControlEmailDb.ResetState();
    }
}