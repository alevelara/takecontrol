using Microsoft.EntityFrameworkCore;
using Takecontrol.Shared.Tests.Contracts;
using Takecontrol.Shared.Tests.Utils;
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

    public void EnsureDatabase()
    {
        if (Context.Database.GetPendingMigrations().Any())
            Context.Database.Migrate();
    }

    public async Task ResetState()
    {
        if (await Context.Database.CanConnectAsync())
        {
            Context.Clubs.ExecuteDelete();
            Context.Addresses.ExecuteDelete();
            Context.Players.ExecuteDelete();
        }
    }

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
