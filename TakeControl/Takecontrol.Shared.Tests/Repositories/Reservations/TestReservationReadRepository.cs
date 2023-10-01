using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Tests.Contracts.Reservations;
using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Shared.Tests.Repositories.Reservations;

public class TestReservationReadRepository : ITestReservationReadRepository
{
    private readonly TakeControlMatchesDb _dbContext;

    public TestReservationReadRepository(TakeControlMatchesDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Reservation?> GetReservationByCourtAsync(Guid courtId)
    {
        return await _dbContext.Context.Set<Reservation>().FirstOrDefaultAsync(r => r.CourtId == courtId);
    }
}
