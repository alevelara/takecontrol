using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Shared.Tests.Repositories.Matches;

public class TestMatchReadRepository
{
    private readonly TakeControlMatchesDb _dbContext;

    public TestMatchReadRepository(TakeControlMatchesDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Match?> GetMatchById(Guid matchId)
    {
        return await _dbContext.Context.Set<Match>().FirstOrDefaultAsync(r => r.Id == matchId);
    }
}
