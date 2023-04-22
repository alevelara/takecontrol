using Microsoft.EntityFrameworkCore;
using Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.User.Infrastructure.Repositories.Primitives;

namespace Takecontrol.User.Infrastructure.Repositories.PlayerClubs;

public class PlayerClubReadRepository : ReadBaseRepository<PlayerClub>, IPlayerClubReadRepository
{
    private readonly TakeControlDbContext _dbContext;

    public PlayerClubReadRepository(TakeControlDbContext dbContext) : base(dbContext)
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
