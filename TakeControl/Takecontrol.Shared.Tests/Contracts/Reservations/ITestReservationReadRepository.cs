using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Shared.Tests.Contracts.Reservations;

public interface ITestReservationReadRepository
{
    Task<Reservation?> GetReservationByCourtAsync(Guid courtId);
}
