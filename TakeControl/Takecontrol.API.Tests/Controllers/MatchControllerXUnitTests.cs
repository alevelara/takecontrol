using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Takecontrol.API.Routes;
using Takecontrol.API.Tests.Primitives;
using Takecontrol.Matches.Domain.Messages.Matches.Requests;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.User.Domain.Messages.Clubs.Requests;
using Xunit;

namespace Takecontrol.API.Tests.Controllers;

[Collection(SharedTestCollection.Name)]
[Trait("Category", "IntegrationTests")]
public class MatchControllerXUnitTests : IAsyncLifetime
{
    private static string mainEndpoint = "api/v1/match/";
    private static string createMatchEndpoint = mainEndpoint + MatchRouteName.Create;

    private readonly TakeControlMatchesDb _takeControlMatchDb;
    private readonly TestBase _testBase;

    public MatchControllerXUnitTests(ApiWebApplicationFactory<Program> factory)
    {
        _takeControlMatchDb = factory.TakeControlMatchesDb;
        _testBase = new TestBase(factory);
    }
    #region

    [Fact]
    public async Task CreateMatch_Should_CreateAMatchSuccesfully_WhenAReservationIsAvailable()
    {
        var court = await CreateCourtForTest();
        var reservation = await CreateReservationForTest(court.Id, true);
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        var request = new CreateMatchRequest(Guid.NewGuid(), reservation.Id);

        var response = await httpClient.PostAsJsonAsync(createMatchEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateMatch_Should_Fail_WhenTheReservationIsNotAvailable()
    {
        var court = await CreateCourtForTest();
        var reservation = await CreateReservationForTest(court.Id, false);
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        var request = new CreateMatchRequest(Guid.NewGuid(), reservation.Id);

        var response = await httpClient.PostAsJsonAsync(createMatchEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task CreateMatch_Should_Fail_WhenReservationDoesNotExist()
    {
        var httpClient = await AddJWTTokenToHeaderForPlayers();

        var request = new CreateMatchRequest(Guid.NewGuid(), Guid.NewGuid());

        var response = await httpClient.PostAsJsonAsync(createMatchEndpoint, request, default);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    #endregion

    #region Private Methods

    private async Task<Reservation> CreateReservationForTest(Guid courtId, bool isAvailable)
    {
        var reservation = Reservation.Create(courtId, new TimeOnly(10, 0), new TimeOnly(11, 30), DateOnly.FromDateTime(DateTime.Now), isAvailable);
        await _takeControlMatchDb.Context.Set<Reservation>().AddAsync(reservation);
        await _takeControlMatchDb.Context.SaveChangesAsync();
        return reservation;
    }

    private async Task<Court> CreateCourtForTest()
    {
        var court = Court.Create(Guid.NewGuid(), "Test 1");
        await _takeControlMatchDb.Context.Set<Court>().AddAsync(court);
        await _takeControlMatchDb.Context.SaveChangesAsync();
        return court;
    }

    private async Task<HttpClient> AddJWTTokenToHeaderForPlayers()
    {
        return await _testBase.RegisterSecuredUserAsPlayerAsync();
    }

    #endregion

    public async Task DisposeAsync()
    {
        await _takeControlMatchDb.ResetState();
    }

    public Task InitializeAsync() => Task.CompletedTask;
}
