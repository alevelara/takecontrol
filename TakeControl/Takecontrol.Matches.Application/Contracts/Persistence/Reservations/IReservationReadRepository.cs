using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Matches.Application.Contracts.Persistence.Reservations;

public interface IReservationReadRepository
{
    Task<Reservation?> GetReservationById(Guid reservationId);
}