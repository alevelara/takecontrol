using Microsoft.EntityFrameworkCore;
using Takecontrol.Application.Contracts.Persitence.Players;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Identity;

namespace Takecontrol.Infrastructure.Repositories.Primitives.Players;

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
}