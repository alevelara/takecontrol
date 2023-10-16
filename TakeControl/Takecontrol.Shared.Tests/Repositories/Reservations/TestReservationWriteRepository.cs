using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Shared.Tests.Repositories.Reservations;

public class TestReservationWriteRepository
{
    private readonly TakeControlMatchesDb _dbContext;

    public TestReservationWriteRepository(TakeControlMatchesDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        await _dbContext.Context.Set<Reservation>().AddAsync(reservation);
        await _dbContext.Context.SaveChangesAsync();
    }
}
