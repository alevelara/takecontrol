using Microsoft.EntityFrameworkCore;
using Takecontrol.Application.Contracts.Persitence.Clubs;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Identity;

namespace Takecontrol.Infrastructure.Repositories.Primitives.Clubs;

public class ClubReadRepository : ReadBaseRepository<Club>, IClubReadRepository
{
    private readonly TakeControlDbContext _dbContext;

    public ClubReadRepository(TakeControlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Club>> GetAllClubsAsync()
    {
        return await _dbContext.Clubs!
            .Include(c => c.Address)
            .IgnoreAutoIncludes<Club>()
            .ToListAsync();
    }

    public async Task<Club?> GetClubByCodeAndClubId(Guid clubId, string code)
    {
        return await _dbContext.Clubs!
            .Include(c => c.Address)
            .IgnoreAutoIncludes<Club>()
            .FirstOrDefaultAsync(c => c.Id == clubId && c.Code == code);
    }

    public async Task<Club?> GetClubByUserId(Guid userId)
    {
        return await _dbContext.Clubs!
            .Include(c => c.Address)
            .IgnoreAutoIncludes<Club>()
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
}
