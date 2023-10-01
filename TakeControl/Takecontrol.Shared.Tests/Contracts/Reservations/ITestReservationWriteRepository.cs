using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Shared.Tests.Contracts.Reservations;

public interface ITestReservationWriteRepository
{
    Task AddReservationAsync(Reservation reservation);
}
