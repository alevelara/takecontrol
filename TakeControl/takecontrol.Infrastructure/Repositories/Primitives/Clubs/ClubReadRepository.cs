using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories.Primitives.Clubs;

public class ClubReadRepository : ReadRepositoryBase<Club>, IClubReadRepository
{
    private readonly TakeControlDbContext _dbContext;

    public ClubReadRepository(TakeControlDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Club> GetClubById(Guid id)
    {
        return await _dbContext.Clubs.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id);
    }
}
