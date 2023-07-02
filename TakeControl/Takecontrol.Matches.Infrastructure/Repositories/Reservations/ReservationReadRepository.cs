using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.Matches.Infrastructure.Repositories.Reservations;

public sealed class ReservationReadRepository : IReservationReadRepository
{
    private readonly MatchesDbContext _context;

    public ReservationReadRepository(MatchesDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetReservationById(Guid reservationId)
    {
        return await _context
            .Set<Reservation>().
            FirstOrDefaultAsync(r => r.Id == reservationId);
    }

    public async Task<bool> IsReservationAvailable(Guid reservationId)
    {
        var reservation = await _context.Set<Reservation>().FindAsync(reservationId);
        return reservation!.IsAvailable;
    }
}
