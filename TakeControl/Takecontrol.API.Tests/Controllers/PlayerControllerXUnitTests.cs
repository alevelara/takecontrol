using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using Takecontrol.API.Tests.Controllers.TestData;
using Takecontrol.API.Tests.Helpers;
using Takecontrol.API.Tests.Primitives;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.User.Domain.Messages.Players.Requests;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Domain.Models.Players.Enums;
using Xunit;
using Xunit.Priority;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;
using Player = Takecontrol.User.Domain.Models.Players.Player;

namespace Takecontrol.API.Tests.Controllers;

[Trait("Category", Category.APIIntegrationTests)]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(30)]
[Collection(SharedTestCollection.Name)]
public class PlayerControllerXUnitTests : IAsyncLifetime
{
    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly TakeControlMatchesDb _takeControlMatchesDb;
    private readonly HttpClient _httpClient;
    private readonly TestBase _testBase;

    public PlayerControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
        _takeControlMatchesDb = factory.TakeControlMatchesDb;
        _testBase = new TestBase(factory);
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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

            var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);
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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var response = await _httpClient.PostAsJsonAsync(Endpoints.RegisterPlayer, request, default);

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

        var club = await GetClubByNameAsync("nameTest");
        var player = await GetPlayerByNameAsync("nameTest");

        var request = new JoinToClubRequest(player!.Id, club!.Id, club.Code);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToClub, request, default);

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
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToClub, request, default);

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

        var club = await GetClubByNameAsync("nameTest");
        var player = await GetPlayerByNameAsync("nameTest");
        var incorrectCode = "12345";
        var request = new JoinToClubRequest(player!.Id, club!.Id, incorrectCode);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToClub, request, default);

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
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await GetReservationByCourtAsync(court!.Id);
        var player = await GetPlayerByNameAsync("nameTest");
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);

        var request = new JoinToAMatchRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToMatch, request, default);

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
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_Match_Is_Not_Found()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");

        var request = new JoinToAMatchRequest(player!.UserId, Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_Match_Is_Closed()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await GetReservationByCourtAsync(court!.Id);
        var match = await AddClosedMatchForTest(reservation!.Id, player!.UserId);

        var request = new JoinToAMatchRequest(player!.UserId, match!.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Should_Fail_When_Player_Was_Previously_Joint()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await GetReservationByCourtAsync(court!.Id);
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);

        var request = new JoinToAMatchRequest(player!.UserId, match!.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        await httpClient.PostAsJsonAsync(Endpoints.JoinToMatch, request, default);
        var response = await httpClient.PostAsJsonAsync(Endpoints.JoinToMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
    #endregion

    #region CancelMatch tests
    [Fact]
    public async Task Should_fail_when_user_does_not_exist()
    {
        //Arrange
        var request = new CancelMatchByPlayerRequest(Guid.NewGuid(), Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_match_does_not_exist()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        var request = new CancelMatchByPlayerRequest(player!.UserId, Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_other_player_tries_to_cancel_the_match()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await GetReservationByCourtAsync(court!.Id);
        var match = await AddMatchForTest(reservation!.Id, Guid.NewGuid());

        var request = new CancelMatchByPlayerRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_reservation_time_is_smaller_than_one_hour()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await AddReservationForCourt(court!.Id, new TimeOnly(DateTime.Now.AddMinutes(50).Hour, 0), DateOnly.FromDateTime(DateTime.Now));
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);

        var request = new CancelMatchByPlayerRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Should_cancel_the_match_succesfully()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await AddReservationForCourt(court!.Id, new TimeOnly(DateTime.Now.AddHours(3).Hour, 0), DateOnly.FromDateTime(DateTime.Now.AddDays(1)));
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);

        var request = new CancelMatchByPlayerRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PutAsJsonAsync(Endpoints.CancelMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    #endregion

    #region UnsubscribeFromMatch tests
    [Fact]
    public async Task Should_fail_when_player_was_not_registered_previously()
    {
        //Arrange
        var request = new UnsubscribeFromMatchRequest(Guid.NewGuid(), Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnsubscribeFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_if_match_was_not_created_previously()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        var request = new UnsubscribeFromMatchRequest(player!.UserId, Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnsubscribeFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_player_tries_to_unbsubscribe_from_the_match_within_one_day()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await AddReservationForCourt(court!.Id, new TimeOnly(DateTime.UtcNow.Hour), DateOnly.FromDateTime(DateTime.Now));
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);
        var request = new UnsubscribeFromMatchRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnsubscribeFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_subscribe_to_the_match_previously()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await AddReservationForCourt(court!.Id, new TimeOnly(DateTime.UtcNow.AddDays(-2).Hour), DateOnly.FromDateTime(DateTime.Now.AddDays(-2)));
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);
        var request = new UnsubscribeFromMatchRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnsubscribeFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_be_unbsubscribed_from_the_match_and_match_must_be_available_again()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var court = await GetCourtByClubAsync(club!.Id);
        var reservation = await AddReservationForCourt(court!.Id, new TimeOnly(DateTime.UtcNow.AddDays(-2).Hour), DateOnly.FromDateTime(DateTime.Now.AddDays(-2)));
        var match = await AddMatchForTest(reservation!.Id, player!.UserId);
        await AddPlayerMatchForTest(match.Id, player.Id);
        var request = new UnsubscribeFromMatchRequest(player!.UserId, match.Id);
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnsubscribeFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    #endregion

    #region UnregisgerFromClub tests
    [Fact]
    public async Task Should_fail_when_player_was_not_registered_yet()
    {
        //Arrange
        var request = new UnregisterFromClubRequest(Guid.NewGuid(), Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnregisterFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_joint_to_the_club()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        var request = new UnregisterFromClubRequest(player!.UserId, Guid.NewGuid());
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnregisterFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Should_unregister_the_player_when_player_was_previously_registered_to_the_club()
    {
        //Arrange
        await PlayerTestData.RegisterPlayerForTest(_httpClient);
        var player = await GetPlayerByNameAsync("nameTest");
        await ClubTestData.RegisterClubForTest(_httpClient);
        var club = await GetClubByNameAsync("nameTest");
        var httpClient = await AuthTestHelper.AddJWTTokenToHeaderForPlayers(_testBase);
        await PlayerTestData.JoinAPlayerToClub(httpClient, player!.Id, club!.Id, club!.Code);

        var request = new UnregisterFromClubRequest(player!.UserId, club!.Id);

        //Act
        var response = await httpClient.PostAsJsonAsync(Endpoints.UnregisterFromMatch, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    #endregion

    #region Private methods

    private async Task<Match> AddMatchForTest(Guid reservationId, Guid playerId)
    {
        var match = Match.Create(reservationId, playerId);
        await _takeControlMatchesDb.Context.Set<Match>().AddAsync(match);
        await _takeControlMatchesDb.Context.SaveChangesAsync();
        return match;
    }

    private async Task<Match> AddClosedMatchForTest(Guid reservationId, Guid playerId)
    {
        var match = Match.Create(reservationId, playerId);
        match.Close();
        await _takeControlMatchesDb.Context.Set<Match>().AddAsync(match);
        await _takeControlMatchesDb.Context.SaveChangesAsync();
        return match;
    }

    private async Task<Reservation> AddReservationForCourt(Guid courtId, TimeOnly startTime, DateOnly reservationDate)
    {
        var reservation = Reservation.Create(courtId, startTime, new TimeOnly(DateTime.UtcNow.AddHours(2).Hour), reservationDate);
        await _takeControlMatchesDb.Context.Set<Reservation>().AddAsync(reservation);
        await _takeControlMatchesDb.Context.SaveChangesAsync();

        return reservation;
    }

    private async Task<Club?> GetClubByNameAsync(string name)
    {
        return await _takeControlDb.Context.Set<Club>().FirstOrDefaultAsync(x => x.Name == name);
    }

    private async Task<Player?> GetPlayerByNameAsync(string name)
    {
        return await _takeControlDb.Context.Set<Player>().FirstOrDefaultAsync(x => x.Name == name);
    }

    private async Task<Court?> GetCourtByClubAsync(Guid clubId)
    {
        return await _takeControlMatchesDb.Context.Set<Court>().FirstOrDefaultAsync(x => x.ClubId == clubId);
    }

    private async Task<Reservation?> GetReservationByCourtAsync(Guid courtId)
    {
        return await _takeControlMatchesDb.Context.Reservations.FirstOrDefaultAsync(x => x.CourtId == courtId);
    }

    private async Task<MatchPlayer?> AddPlayerMatchForTest(Guid matchId, Guid playerId)
    {
        var matchPlayer = MatchPlayer.Create(matchId, playerId);
        await _takeControlMatchesDb.Context.Set<MatchPlayer>().AddAsync(matchPlayer);
        await _takeControlMatchesDb.Context.SaveChangesAsync();
        return matchPlayer;
    }

    #endregion

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlIdentityDb.ResetState();
        await _takeControlDb.ResetState();
        await _takeControlEmailDb.ResetState();
        await _takeControlMatchesDb.ResetState();
    }
}