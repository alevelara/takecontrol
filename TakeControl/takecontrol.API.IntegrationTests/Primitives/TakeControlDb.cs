using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Contracts;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class TakeControlDb : IDbConfiguration
{
    public TakeControlDbContext Context { get; private set; }

    public TakeControlDb(TakeControlDbContext context)
    {
        Context = context;
    }

    public async Task EnsureDatabase()
    {
        await this.Context.Database.MigrateAsync();
    }

    public async Task ResetState()
    {
        await this.Context.Clubs.ExecuteDeleteAsync();
        await this.Context.Addresses.ExecuteDeleteAsync();
        await this.Context.Players.ExecuteDeleteAsync();
    }
}
