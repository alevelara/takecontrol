using Takecontrol.Matches.Domain.Models.Matches.Documents;

namespace Takecontrol.Matches.Application.Contracts.Persistence.MatchesInfo;

public interface IMatchInfoWriteRepository
{
    Task CreateAsync(MatchInfo matchInfo, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, MatchInfo matchInfo, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
