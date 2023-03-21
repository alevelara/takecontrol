using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence.PlayerClubs;
using takecontrol.Domain.Models.PlayerClubs;
using takecontrol.Domain.Models.Players;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories.Primitives.PlayerClubs;

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
