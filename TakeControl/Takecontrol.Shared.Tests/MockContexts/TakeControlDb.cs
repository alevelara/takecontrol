using Microsoft.EntityFrameworkCore;
using Takecontrol.Shared.Tests.Contracts;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.Shared.Tests.MockContexts;

public class TakeControlDb : IDbConfiguration
{
    public TakeControlDbContext Context { get; private set; }

    public TakeControlDb()
    {
        var factory = new TakeControlDBContextFactory();
        Context = factory.CreateDbContext(TestConfigurations.GetConnectionString("ConnectionString"));
    }

    public Task EnsureDatabase()
    {
        Context.Database.Migrate();
        return Task.CompletedTask;
    }

    public async Task ResetState()
    {
        if (await Context.Database.CanConnectAsync())
        {
            await Context.Clubs.ExecuteDeleteAsync();
            await Context.Addresses.ExecuteDeleteAsync();
            await Context.Players.ExecuteDeleteAsync();
        }
    }

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
