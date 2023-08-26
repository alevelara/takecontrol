using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Repositories.Primitives;

namespace Takecontrol.Matches.Infrastructure.Repositories.MatchPlayers;

public class MatchPlayerReadRepository : ReadBaseRepository<MatchPlayer>, IMatchPlayerReadRepository
{
    private readonly MatchesDbContext _context;
    public MatchPlayerReadRepository(MatchesDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<MatchPlayer?> GetMatchPlayerByPlayerIdAndMatchId(Guid playerId, Guid matchId)
    {
        return await _context.MatchPlayers
            .FirstOrDefaultAsync(c => c.MatchId == matchId && c.PlayerId == playerId);
    }

    public async Task<List<MatchPlayer>> GetMatchPlayersByMatchId(Guid matchId)
    {
        return await _context.MatchPlayers
            .Where(c => c.MatchId == matchId)
            .ToListAsync();
    }
}
