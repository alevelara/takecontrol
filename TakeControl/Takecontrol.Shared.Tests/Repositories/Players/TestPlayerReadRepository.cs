using Microsoft.EntityFrameworkCore;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.Shared.Tests.Repositories.Players;

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
