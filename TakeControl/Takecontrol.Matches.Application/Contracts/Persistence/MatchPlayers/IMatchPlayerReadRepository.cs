using Takecontrol.Matches.Domain.Models.MatchPlayers;

namespace Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;

public interface IMatchPlayerReadRepository
{
    Task<List<MatchPlayer>> GetMatchPlayersByMatchId(Guid matchId);
    Task<MatchPlayer?> GetMatchPlayerByPlayerIdAndMatchId(Guid playerId, Guid matchId);
}
