using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Matches.Application.Contracts.Persistence.Reservations;

public interface IReservationReadRepository
{
    Task<bool> IsReservationAvailable(Guid reservationId);

    Task<Reservation?> GetReservationById(Guid reservationId);
}