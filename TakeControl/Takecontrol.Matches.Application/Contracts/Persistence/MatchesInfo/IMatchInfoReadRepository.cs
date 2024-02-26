using System.Linq.Expressions;
using Takecontrol.Matches.Domain.Models.Matches.Documents;

namespace Takecontrol.Matches.Application.Contracts.Persistence.MatchesInfo;

public interface IMatchInfoReadRepository
{
    Task<MatchInfo> GetMatchInfoByIdAsync(Guid matchId);
    Task<List<MatchInfo>> GetAllAsync();
    Task<List<MatchInfo>> GetAsync(Expression<Func<MatchInfo, bool>> predicate);
}
