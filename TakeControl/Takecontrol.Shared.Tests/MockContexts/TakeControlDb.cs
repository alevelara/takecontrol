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
        Context.Database.EnsureCreated();
    }

    public Task ResetState()
    {
        if (Context.Database.CanConnect())
        {
            Context.Clubs.ExecuteDelete();
            Context.Addresses.ExecuteDelete();
            Context.Players.ExecuteDelete();
        }

        return Task.CompletedTask;
    }

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
