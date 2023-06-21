using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.Matches.Infrastructure.Repositories.Matches;

public class MatchReadRepository : IMatchReadRepository
{
    private readonly MatchesDbContext _context;

    public MatchReadRepository(MatchesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsThisReservationCompleted(Guid reservationId)
    {
        return await _context.Set<Match>().AnyAsync(m => m.ReservationId == reservationId);
    }
}
