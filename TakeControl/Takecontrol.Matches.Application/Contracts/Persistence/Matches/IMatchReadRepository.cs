using Takecontrol.Matches.Domain.Models.Matches;

namespace Takecontrol.Matches.Application.Contracts.Persistence.Matches;

public interface IMatchReadRepository
{
    Task<bool> IsThisReservationCompleted(Guid reservationId);
}
