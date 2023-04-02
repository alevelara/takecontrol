using Microsoft.EntityFrameworkCore;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Domain.Models.Players;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.User.Infrastructure.Repositories.Primitives.Players;

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