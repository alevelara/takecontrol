using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Shared.MockContexts;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.IntegrationTest.Shared.Repositories.Clubs;

public class TestClubReadRepository
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
