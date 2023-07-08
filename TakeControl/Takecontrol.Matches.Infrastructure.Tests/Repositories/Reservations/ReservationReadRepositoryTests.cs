using Takecontrol.Matches.Infrastructure.Repositories.Reservations;
using Takecontrol.Matches.Infrastructure.Tests.Mocks;
using Takecontrol.Shared.Tests.Constants;

using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Matches.Infrastructure.Tests.Repositories.Reservations;

[Trait("Category", Category.MatchIntegrationTests)]
[Collection(SharedTestCollection.Name)]
public class ReservationReadRepositoryTests : IAsyncLifetime
{
    private readonly TakeControlMatchesDb _dbContext;

    public ReservationReadRepositoryTests()
    {
        _dbContext = new TakeControlMatchesDb();
    }

    [Fact]
    public async Task Should_return_null_when_reservation_doesnt_exist()
    {
        var readRepository = new ReservationReadRepository(_dbContext.Context);

        var result = await readRepository.GetReservationById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task Should_return_an_available_reservation_when_reservation_exists_in_database()
    {
        var court = await MockCourtRepository.AddCourt(_dbContext.Context);
        var reservation = await MockReservationRepository.AddReservation(_dbContext.Context, court.Id);
        var readRepository = new ReservationReadRepository(_dbContext.Context);

        var result = await readRepository.GetReservationById(reservation.Id);

        Assert.NotNull(result);
        Assert.Equal(reservation.Id, result.Id);
        Assert.True(reservation.IsAvailable);
    }

    [Fact]
    public async Task Should_return_an_unavailable_reservation_when_reservation_exists_in_database()
    {
        var court = await MockCourtRepository.AddCourt(_dbContext.Context);
        var reservation = await MockReservationRepository.AddUnavaibleReservation(_dbContext.Context, court.Id);
        var readRepository = new ReservationReadRepository(_dbContext.Context);

        var result = await readRepository.GetReservationById(reservation.Id);

        Assert.NotNull(result);
        Assert.Equal(reservation.Id, result.Id);
        Assert.False(reservation.IsAvailable);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _dbContext.ResetState();
}
