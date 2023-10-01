using Microsoft.EntityFrameworkCore;
using Takecontrol.Shared.Tests.Contracts.Clubs;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.Shared.Tests.Repositories.Clubs;

public class TestClubReadRepository : ITestClubReadRepository
{
    private readonly TakeControlDb _takeControlDb;

    public TestClubReadRepository(TakeControlDb takeControlDb)
    {
        _takeControlDb = takeControlDb;
    }

    public async Task<Club?> GetClubByName(string name)
    {
        return await _takeControlDb.Context.Set<Club>().FirstOrDefaultAsync(c => c.Name == name);
    }
}
