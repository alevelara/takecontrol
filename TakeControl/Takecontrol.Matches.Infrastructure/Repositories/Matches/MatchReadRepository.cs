using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Repositories.Primitives;

namespace Takecontrol.Matches.Infrastructure.Repositories.Matches;

public class MatchReadRepository : ReadBaseRepository<Match>, IMatchReadRepository
{
    private readonly MatchesDbContext _context;

    public MatchReadRepository(MatchesDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> IsThisReservationCompleted(Guid reservationId)
    {
        return await _context.Set<Match>().AnyAsync(m => m.ReservationId == reservationId);
    }

    public async Task<Match?> GetMatchWithReservation(Guid matchId)
    {
        return await _context.Set<Match>()
            .Include(m => m.Reservation)
            .FirstOrDefaultAsync(c => c.Id == matchId);
    }
}
