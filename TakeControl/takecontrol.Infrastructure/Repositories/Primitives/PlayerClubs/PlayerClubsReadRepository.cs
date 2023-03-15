using Microsoft.EntityFrameworkCore;
using Takecontrol.Application.Contracts.Persitence.PlayerClubs;
using Takecontrol.Domain.Models.PlayerClubs;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Identity;

namespace Takecontrol.Infrastructure.Repositories.Primitives.PlayerClubs;

public class PlayerClubsReadRepository : ReadBaseRepository<Player>, IPlayerClubsReadRepository
{
    private readonly TakeControlDbContext _dbContext;

    public PlayerClubsReadRepository(TakeControlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PlayerClub>> GetAllPlayersByClubId(Guid ClubId)
    {
        return await _dbContext.PlayerClubs!
            .Include(c => c.Player)
            .Where(b => b.ClubId == ClubId)
            .ToListAsync();
    }
}
