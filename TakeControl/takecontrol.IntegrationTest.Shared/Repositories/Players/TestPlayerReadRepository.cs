using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Shared.MockContexts;
using takecontrol.Domain.Models.Players;

namespace takecontrol.IntegrationTest.Shared.Repositories.Players;

public class TestPlayerReadRepository
{
    private readonly TakeControlDb _takeControlDb;

    public TestPlayerReadRepository(TakeControlDb takeControlDb)
    {
        _takeControlDb = takeControlDb;
    }

    public async Task<Player?> GetPlayerByName(string name)
    {
        return await _takeControlDb.Context.Set<Player>().FirstOrDefaultAsync(c => c.Name == name);
    }
}
