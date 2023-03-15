using System.Net;
using System.Net.Http.Json;
using Takecontrol.API.IntegrationTests.Primitives;
using Takecontrol.API.IntegrationTests.Shared.MockContexts;
using Takecontrol.Domain.Messages.Clubs;
using Takecontrol.Domain.Messages.Players;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Domain.Models.Players.Enums;
using Takecontrol.IntegrationTest.Shared.Repositories.Clubs;
using Takecontrol.IntegrationTest.Shared.Repositories.Players;
using Xunit.Priority;

namespace Takecontrol.API.IntegrationTests.Controllers;

[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(30)]
[Collection(SharedTestCollection.Name)]
public class PlayerControllerXUnitTests : IAsyncLifetime
{
    private const string RegisterPlayerEndpoint = "api/v1/player/Register";
    private const string RegisterClubEndpoint = "api/v1/club/Register";
    private const string JoinToClubEndpoint = "api/v1/player/Join";
    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly HttpClient _httpClient;
    private readonly TestBase _testBase;
    private readonly TestClubReadRepository _clubReadRepository;
    private readonly TestPlayerReadRepository _playerReadRepository;

    public PlayerControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
        _testBase = new TestBase(factory);
        _clubReadRepository = new TestClubReadRepository(_takeControlDb);
        _playerReadRepository = new TestPlayerReadRepository(_takeControlDb);
    }

    #region RegisterPlayer Tests
    [Fact]
    [Priority(29)]
    public async Task RegisterPlayer_Should_Return201StatusCode_WhenRegisterRequestIsValid()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email2@test.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_ShouldReturnCorrectLevel_WhenRegisterRequestIsValid()
    {
        List<string> names = new List<string>();

        foreach (PlayerLevel level in Enum.GetValues(typeof(PlayerLevel)))
        {
            var request = new RegisterPlayerRequest
            {
                Email = $"email2{(int)level}@test.com",
                Name = $"nameTest2-{(int)level}",
                Password = "Password123!",
                AvgNumberOfMatchesInAWeek = 2,
                NumberOfClassesInAWeek = 1,
                NumberOfYearsPlayed = (int)level * (int)level,
            };

            var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);
            names.Add(request.Name);
        }

        List<Player> players = _takeControlDb.Context.Players.ToList();

        Assert.Equal(names.Count, players.Count);

        foreach (string name in names)
        {
            int level = int.Parse(name.Split('-')[1]);

            Player player = players!.FirstOrDefault(c => c.Name == name);
            Assert.Equal((int)player.PlayerLevel, level);
        }
    }

    [Fact]
    [Priority(39)]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenUserWithSameEmailAlreadyExist()
    {
        await this.RegisterPlayerForTest();

        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenEmailIsIncorrect()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenPasswordIsIncorrect()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "nameTest",
            Password = "password!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenAvgNumberOfMatchesInAWeekIsNegative()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = -1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenNumberOfClassesInAWeekIsNegative()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = -1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenNumberOfYearsPlayedIsNegative()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = -1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterPlayer_Should_ReturnConflict_WhenNameIsEmpty()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    #endregion

    #region JoinToClub Tests
    [Fact]
    public async Task JoinToClub_Should_ReturnCreatedStatusCode_WhenAPlayerJoinToANewClub()
    {
        //Arrange
        await this.RegisterPlayerForTest();
        await this.RegisterClubForTest();

        var club = await _clubReadRepository.GetClubByName("nameTest");
        var player = await _playerReadRepository.GetPlayerByName("nameTest");

        var request = new JoinToClubRequest(player.Id, club.Id, club.Code);
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        //Act
        var response = await httpClient.PostAsJsonAsync<JoinToClubRequest>(JoinToClubEndpoint, request, default);

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
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        //Act
        var response = await httpClient.PostAsJsonAsync<JoinToClubRequest>(JoinToClubEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task JoinToClub_Should_ReturnConflictStatusCode_WhenACodeDoesntMatchWithTheClub()
    {
        //Arrange
        await this.RegisterPlayerForTest();
        await this.RegisterClubForTest();

        var club = await _clubReadRepository.GetClubByName("nameTest");
        var player = await _playerReadRepository.GetPlayerByName("nameTest");
        var incorrectCode = "12345";
        var request = new JoinToClubRequest(player.Id, club.Id, incorrectCode);
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        //Act
        var response = await httpClient.PostAsJsonAsync<JoinToClubRequest>(JoinToClubEndpoint, request, default);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
    #endregion

    #region Private methods
    private async Task RegisterPlayerForTest()
    {
        var request = new RegisterPlayerRequest
        {
            Email = "email@test.com",
            Name = "nameTest",
            Password = "Password123!",
            AvgNumberOfMatchesInAWeek = 1,
            NumberOfClassesInAWeek = 1,
            NumberOfYearsPlayed = 1,
        };

        await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(RegisterPlayerEndpoint, request, default);
    }

    private async Task RegisterClubForTest()
    {
        var request = new RegisterClubRequest
        {
            Email = "club@test.com",
            Name = "nameTest",
            Password = "Password123!",
            City = "City",
            MainAddress = "mainAddress",
            Province = "province"
        };

        await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(RegisterClubEndpoint, request, default);
    }

    private async Task<HttpClient> AddJWTTokenToHeaderForPlayers()
    {
        return await _testBase.RegisterSecuredUserAsPlayerAsync();
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
