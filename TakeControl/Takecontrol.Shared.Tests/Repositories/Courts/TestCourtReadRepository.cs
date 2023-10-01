using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Shared.Tests.Contracts.Courts;
using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Shared.Tests.Repositories.Courts;

public class TestCourtReadRepository : ITestCourtReadRepository
{
    private readonly TakeControlMatchesDb _dbContext;

    public TestCourtReadRepository(TakeControlMatchesDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Court?> GetCourtByClubAsync(Guid clubId)
    {
        return await _dbContext.Context.Set<Court>().FirstOrDefaultAsync(c => c.ClubId == clubId);
    }
}
