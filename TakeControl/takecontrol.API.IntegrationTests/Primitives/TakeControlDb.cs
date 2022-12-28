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
        this.Context.Clubs.RemoveRange(Context.Clubs);
        this.Context.Addresses.RemoveRange(Context.Addresses);
        this.Context.Players.RemoveRange(Context.Players);
        await this.Context.SaveChangesAsync();
    }
}
