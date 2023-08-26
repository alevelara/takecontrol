using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Models.Matches;

namespace Takecontrol.Matches.Application.Contracts.Persistence.Matches;

public interface IMatchReadRepository : IAsyncReadRepository<Match>
{
    Task<bool> IsThisReservationCompleted(Guid reservationId);
}
