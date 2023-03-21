using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence.Players;
using takecontrol.Domain.Models.Players;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories.Primitives.Players;

public class PlayerReadRepository : ReadBaseRepository<Player>, IPlayerReadRepository
{
    private readonly TakeControlDbContext _dbContext;

    public PlayerReadRepository(TakeControlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Player?> GetPlayerByUserId(Guid userId)
    {
        return await _dbContext.Players!
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public Task<List<Player>> GetAllPlayersByClubId(Guid ClubId)
    {
        var players = _dbContext.Players.Where(b => b.PlayerClubs.Any(c => c.ClubId == ClubId));

        return players.ToListAsync();
    }
}