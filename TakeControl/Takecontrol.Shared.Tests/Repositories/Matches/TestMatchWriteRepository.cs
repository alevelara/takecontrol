using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Shared.Tests.Contracts.Matches;
using Takecontrol.Shared.Tests.MockContexts;

namespace Takecontrol.Shared.Tests.Repositories.Matches;

public class TestMatchWriteRepository : ITestMatchWriteRepository
{
    private readonly TakeControlMatchesDb _dbContext;

    public TestMatchWriteRepository(TakeControlMatchesDb dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddMatchAsync(Match match)
    {
        await _dbContext.Context.Set<Match>().AddAsync(match);
        await _dbContext.Context.SaveChangesAsync();
    }
}
