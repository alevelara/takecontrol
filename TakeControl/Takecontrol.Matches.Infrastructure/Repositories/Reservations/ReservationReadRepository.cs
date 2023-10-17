using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Repositories.Primitives;

namespace Takecontrol.Matches.Infrastructure.Repositories.Reservations;

public sealed class ReservationReadRepository : ReadBaseRepository<Reservation>, IReservationReadRepository
{
    private readonly MatchesDbContext _context;

    public ReservationReadRepository(MatchesDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetReservationById(Guid reservationId)
    {
        return await _context
            .Set<Reservation>()
            .FirstOrDefaultAsync(r => r.Id == reservationId);
    }

    public async Task<Reservation?> GetReservationWithCourtById(Guid reservationId)
    {
        return await _context
            .Set<Reservation>()
            .Include(c => c.Court)
            .FirstOrDefaultAsync(r => r.Id == reservationId);
    }
}
