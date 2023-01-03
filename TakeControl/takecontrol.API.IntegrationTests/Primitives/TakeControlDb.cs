using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Contracts;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class TakeControlDb : IDbConfiguration
{
    public TakeControlDbContext Context { get; private set; }

    public TakeControlDb(string connectionString)
    {
        var factory = new TakeControlDBContextFactory();
        Context = factory.CreateDbContext(connectionString);
    }

    public void EnsureDatabase()
    {
        this.Context.Database.Migrate();
    }

    public async Task ResetState()
    {
        if (await this.Context.Database.CanConnectAsync())
        {
            await this.Context.Clubs.ExecuteDeleteAsync();
            await this.Context.Addresses.ExecuteDeleteAsync();
            await this.Context.Players.ExecuteDeleteAsync();
        }
    }
}
