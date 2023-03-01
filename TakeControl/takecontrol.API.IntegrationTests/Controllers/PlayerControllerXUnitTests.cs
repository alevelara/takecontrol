using System.Net;
using System.Net.Http.Json;
using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Messages.Players;
using takecontrol.Domain.Models.Players.Enums;
using Xunit.Priority;
using System.Linq;
using takecontrol.Domain.Models.Players;

namespace takecontrol.API.IntegrationTests.Controllers;

[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(30)]
[Collection(SharedTestCollection.Name)]
public class PlayerControllerXUnitTests : IAsyncLifetime
{
    public static string REGISTER_ENDPOINT = "api/v1/player/Register";
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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

            var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);
            names.Add(request.Name);
        }

        List<Player> elements = _takeControlDb.Context.Players.ToList();

        Assert.Equal(names.Count, elements.Count);

        foreach (string name in names)
        {
            Int32 level = Int32.Parse(name.Split('-')[1]);

            Player element = elements.FirstOrDefault(c => c.Name == name);
            Assert.Equal((int)element.PlayerLevel, level);
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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

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

        var response = await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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

        await this._httpClient.PostAsJsonAsync<RegisterPlayerRequest>(REGISTER_ENDPOINT, request, default);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _takeControlIdentityDb.ResetState();
        await _takeControlDb.ResetState();
        await _takeControlEmailDb.ResetState();
    }
}
