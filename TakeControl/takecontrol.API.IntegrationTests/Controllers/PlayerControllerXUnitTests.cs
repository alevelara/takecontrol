using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Players;
using takecontrol.Domain.Models.Players.Enums;
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

namespace takecontrol.API.IntegrationTests.Controllers;

[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(30)]
[Collection(SharedTestCollection.Name)]
public class PlayerControllerXUnitTests : IAsyncLifetime
{
    private static string playerMainEndpoint = "api/v1/player/";

    private static string clubMainEndpoint = "api/v1/club/";

    private static string playerRegisterEndpoint = playerMainEndpoint + PlayerRouteName.Register;
    
    private static string getAllPlayersEndpoint = playerMainEndpoint + PlayerRouteName.AllByClubId;
    
    private static string clubRegisterEndpoint = clubMainEndpoint + ClubRouteName.Register;
    
    private readonly TakeControlDb _takeControlDb;
    private readonly TakeControlIdentityDb _takeControlIdentityDb;
    private readonly TakeControlEmailDb _takeControlEmailDb;
    private readonly HttpClient _httpClient;

    public PlayerControllerXUnitTests(CustomWebApplicationFactory<Program> factory)
    {
        _takeControlDb = factory.TakecontrolDb;
        _takeControlIdentityDb = factory.TakeControlIdentityDb;
        _httpClient = factory.HttpClient;
        _takeControlEmailDb = factory.TakeControlEmailDb;
    }

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

            var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);
            names.Add(request.Name);
        }

        List<Player> players = _takeControlDb.Context.Players.ToList();

        Assert.Equal(names.Count, players.Count);

        foreach (string name in names)
        {
            Int32 level = Int32.Parse(name.Split('-')[1]);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /**
    *   GetAllPlayersByClubId
    */
    [Fact]
    public async Task GetAllPlayersByClubId_Should_Return200StatusCode() 
    {
        // var players = Builder<Player>
        //     .CreateNew()
        //     .With(x => x.Id)
        //     .With(x => x.Name)
        //     .With(x => x.NumberOfYearsPlayed)
        //     .Build();
        Builder<PlayerDto>.CreateListOfSize(10).Build().ToList().ForEach(i => 
            Console.WriteLine($"Id = {i.Id} UserId = {i.UserId} Name = {i.Name} NumberOfClasses = {i.NumberOfClassesInAWeek}")
        );

        
        var clubs =  Builder<ClubDto>.CreateListOfSize(3).Build().ToList();
        var numberOfPlayerPerClub = 2;

        List<Guid> clubIds = new List<Guid>();

        foreach(ClubDto club in clubs) {
            numberOfPlayerPerClub *= 3;
            var players = Builder<PlayerDto>.CreateListOfSize(numberOfPlayerPerClub).Build().ToList();

            // var clubRequest = new RegisterClubRequest
            // {
            //     City = club.Address.City,
            //     Email = $"{club.Name}@clubemail.com",
            //     MainAddress = club.Address.MainAddress,
            //     Name = club.Name,
            //     Password = $"{club.Name}123!",
            //     Province = club.Address.Province
            // };

            var clubRequest = new RegisterClubRequest
            {
                City = "Sevilla",
                Email = $"{club.Name}@clubemail.com",
                MainAddress = "Cala",
                Name = club.Name,
                Password = $"{club.Name}123!",
                Province = "club.Address.Province"
            };

            var clubResponse = await this._httpClient.PostAsJsonAsync<RegisterClubRequest>(clubRegisterEndpoint, clubRequest, default);
            
            var clubCreated = await GetClubByCode(club.Code);

            foreach(PlayerDto player in players) {

                var playerRequest = new RegisterPlayerRequest
                {
                    Name = player.Name,
                    Email = $"{player.Name}@clubemail.com",
                    Password = $"{player.Name}123!",
                    AvgNumberOfMatchesInAWeek = player.AvgNumberOfMatchesInAWeek,
                    NumberOfClassesInAWeek = player.NumberOfClassesInAWeek,
                    NumberOfYearsPlayed = player.NumberOfYearsPlayed
                };

                var playerResponse = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, playerRequest, default);

                var playerCreated = await GetPlayerByName(player.Name);
                
                PlayerClub.Create(playerCreated.Id, clubCreated.Id);
                clubIds.Add(clubCreated.Id);
            }
        }

        // Builder<Player>.CreateListOfSize(10).BuildHierarchy(players);

        // Console.WriteLine("Player" + players.Name);

        var responsePlayersClub1 = await _httpClient.GetAsync(getAllPlayersEndpoint + $"?clubId={clubIds[0]}");
        Console.WriteLine("TEST: " + responsePlayersClub1);
        Assert.Equal(1, 1);

    }

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

        await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(clubRegisterEndpoint, request, default);
    }

    private async Task<Club> GetClubByCode(string code)
    {
        return await _takeControlDb.Context.Clubs?.FirstOrDefaultAsync(c => c.Code == code);
    }
    
    private async Task<Player> GetPlayerByName(string name)
    {
        return await _takeControlDb.Context.Players?.FirstOrDefaultAsync(c => c.Name == name);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlIdentityDb.ResetState();
        await _takeControlDb.ResetState();
        await _takeControlEmailDb.ResetState();
    }
}
