using Takecontrol.Matches.Domain.Models.Matches;

namespace Takecontrol.Shared.Tests.Contracts.Matches;

public interface ITestMatchReadRepository
{
    Task<Match?> GetMatchById(Guid matchId);
}
