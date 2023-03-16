using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.API.IntegrationTests.Shared.MockContexts;
using takecontrol.Domain.Messages.Clubs;
using takecontrol.Domain.Messages.Players;
using takecontrol.Domain.Models.Players;
using takecontrol.Domain.Models.Players.Enums;
using takecontrol.IntegrationTest.Shared.Repositories.Clubs;
using takecontrol.IntegrationTest.Shared.Repositories.Players;
using Xunit.Priority;
using System.Linq;
using takecontrol.Domain.Models.Players;
using FizzWare.NBuilder;
using takecontrol.Domain.Dtos.Players;
using takecontrol.Domain.Dtos.Clubs;
using takecontrol.Domain.Messages.Clubs;
using takecontrol.Domain.Models.PlayerClubs;
using takecontrol.API.Routes;
using takecontrol.Domain.Models.Clubs;
using Microsoft.EntityFrameworkCore;
using takecontrol.Domain.Messages.Identity;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace takecontrol.API.IntegrationTests.Controllers;

[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(30)]
[Collection(SharedTestCollection.Name)]
public class PlayerControllerXUnitTests : IAsyncLifetime
{
    private static string playerMainEndpoint = "api/v1/player/";

    private static string clubMainEndpoint = "api/v1/club/";

    private static string loginEndpoint = "api/v1/auth/Login";

    private const string JoinToClubEndpoint = "api/v1/player/Join";

    private static string playerRegisterEndpoint = playerMainEndpoint + PlayerRouteName.Register;

    private static string getAllPlayersEndpoint = playerMainEndpoint + PlayerRouteName.AllByClubId;

    private static string clubRegisterEndpoint = clubMainEndpoint + ClubRouteName.Register;

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

            var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);
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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /**
    *   GetAllPlayersByClubId
    */
    [Fact]
    public async Task GetAllPlayersByClubId_Should_ReturnCorrectNumberOfPlayers()
    {
        int numClubs = 3;
        var numberOfPlayerPerClub = 1;

        Dictionary<Guid, List<RegisterPlayerRequest>> clubBelongToPlayers = await this.RegisterPlayerBelongToCLub(numClubs, numberOfPlayerPerClub);

        // Guid clubId = clubBelongToPlayers.First().Key;
        RegisterPlayerRequest player = clubBelongToPlayers.First().Value.First();

        // Get token
        string token = await this.GetTokenLogin(player.Email, player.Password);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Flatten
        var allPlayersCreated = clubBelongToPlayers.Values
                     .SelectMany(x => x)
                     .ToList();

        // Reading Players by ClubId
        foreach (Guid clubId in clubBelongToPlayers.Keys)
        {
            var responsePlayersClub = await _httpClient.GetAsync(getAllPlayersEndpoint + $"?clubId={clubId}");
            var strPlayers = await responsePlayersClub.Content.ReadAsStringAsync();

            JArray jsonBodyPlayers = JArray.Parse(strPlayers);
            var playersPerClubCount = jsonBodyPlayers.Count;

            Assert.Equal(clubBelongToPlayers.FirstOrDefault(x => x.Key == clubId).Value.Count, playersPerClubCount);
            Assert.NotEqual(allPlayersCreated.Count, playersPerClubCount);
            Assert.Equal(HttpStatusCode.OK, responsePlayersClub.StatusCode);
        }
    }

    [Fact]
    public async Task GetAllPlayersByClubId_Return_BadRequest()
    {
        int numClubs = 1;
        var numberOfPlayerPerClub = 1;

        Dictionary<Guid, List<RegisterPlayerRequest>> clubBelongToPlayers = await this.RegisterPlayerBelongToCLub(numClubs, numberOfPlayerPerClub);

        Guid clubId = clubBelongToPlayers.First().Key;
        RegisterPlayerRequest player = clubBelongToPlayers.First().Value.First();

        // Get token
        string token = await this.GetTokenLogin(player.Email, player.Password);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Reading Player by ClubId
        var responsePlayersClub = await _httpClient.GetAsync(getAllPlayersEndpoint);
        Assert.Equal(HttpStatusCode.BadRequest, responsePlayersClub.StatusCode);
    }

    [Fact]
    public async Task GetAllPlayersByClubId_Return_Unauthorized()
    {
        int numClubs = 1;
        var numberOfPlayerPerClub = 1;

        Dictionary<Guid, List<RegisterPlayerRequest>> clubBelongToPlayers = await this.RegisterPlayerBelongToCLub(numClubs, numberOfPlayerPerClub);

        // Guid clubId = clubBelongToPlayers.First().Key;
        Guid clubId = clubBelongToPlayers.First().Key;
        RegisterPlayerRequest player = clubBelongToPlayers.First().Value.First();

        // Reading Player by ClubId - Reset token

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "token-test");
        var responsePlayersClub = await _httpClient.GetAsync(getAllPlayersEndpoint + $"?clubId={clubId}");
        Assert.Equal(HttpStatusCode.Unauthorized, responsePlayersClub.StatusCode);
    }

    private async Task<Dictionary<Guid, List<RegisterPlayerRequest>>> RegisterPlayerBelongToCLub(int numClubs, int numberOfPlayerPerClub)
    {
        var clubs = Builder<ClubDto>.CreateListOfSize(numClubs).Build().ToList();

        Dictionary<Guid, List<RegisterPlayerRequest>> clubIdsPlayers = new Dictionary<Guid, List<RegisterPlayerRequest>>();
        var clubIndex = 1;

        foreach (ClubDto club in clubs)
        {
            var players = Builder<PlayerDto>.CreateListOfSize(numberOfPlayerPerClub).Build().ToList();
            var clubRequest = new RegisterClubRequest
            {
                City = "Sevilla",
                Email = $"{club.Name}playertest@clubemail.com",
                MainAddress = "Calle Test",
                Name = club.Name,
                Password = $"{club.Name}123!",
                Province = "club.Address.Province"
            };

            var clubResponse = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(clubRegisterEndpoint, clubRequest, default);

            var clubCreated = await GetClubByName(club.Name);
            List<RegisterPlayerRequest> playersCreated = new List<RegisterPlayerRequest>();

            foreach (PlayerDto player in players)
            {
                var playerRequest = new RegisterPlayerRequest
                {
                    Name = player.Name,
                    Email = $"{player.Name}-{club.Name}@playeremail.com",
                    Password = $"{player.Name}123!",
                    AvgNumberOfMatchesInAWeek = player.AvgNumberOfMatchesInAWeek,
                    NumberOfClassesInAWeek = player.NumberOfClassesInAWeek,
                    NumberOfYearsPlayed = player.NumberOfYearsPlayed
                };
                var playerResponse = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, playerRequest, default);
                Assert.Equal(HttpStatusCode.OK, playerResponse.StatusCode);
                var playerCreated = await GetPlayerByName(player.Name);
                var playerClubRel = PlayerClub.Create(playerCreated.Id, clubCreated.Id);
                _takeControlDb.Context.PlayerClubs.Add(playerClubRel);

                playersCreated.Add(playerRequest);
            }

            clubIdsPlayers[clubCreated.Id] = playersCreated;
            numberOfPlayerPerClub *= 2;
            clubIndex += 1;
        }

        // Saving relation between club and player
        _takeControlDb.Context.SaveChanges();

        return clubIdsPlayers;
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

        await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(playerRegisterEndpoint, request, default);
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

        await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(clubRegisterEndpoint, request, default);
    }

    private async Task<HttpClient> AddJWTTokenToHeaderForPlayers()
    {
        return await _testBase.RegisterSecuredUserAsPlayerAsync();
    }

    private async Task<Club> GetClubByName(string name)
    {
        return await _takeControlDb.Context.Clubs?.FirstOrDefaultAsync(c => c.Name == name);
    }

    private async Task<Player> GetPlayerByName(string name)
    {
        return await _takeControlDb.Context.Players?.FirstOrDefaultAsync(c => c.Name == name);
    }

    private async Task<string> GetTokenLogin(string email, string password)
    {
        // Get Player by current club id
        var request = new AuthRequest
        {
            Email = email,
            Password = password,
        };

        // Reading Login response
        var responseLogin = await this._httpClient.PostAsJsonAsync<AuthRequest>(loginEndpoint, request, CancellationToken.None);
        var strBody = await responseLogin.Content.ReadAsStringAsync();
        JObject jsonBody = JObject.Parse(strBody);

        Console.WriteLine("JSON: " + jsonBody);

        return jsonBody is not null ? jsonBody["token"].ToString() : "";
    }

    #endregion

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlIdentityDb.ResetState();
        // await _takeControlDb.ResetState();
        await _takeControlEmailDb.ResetState();
    }
}
