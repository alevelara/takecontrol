using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Shared.Tests.Contracts;
using Takecontrol.Shared.Tests.Utils;

namespace Takecontrol.Shared.Tests.MockContexts;

public class TakeControlMatchesDb : IDbConfiguration
{
    public MatchesDbContext Context { get; private set; }

    public TakeControlMatchesDb()
    {
        var factory = new MatchesDbContextFactory();
        Context = factory.CreateDbContext(TestConfigurations.GetConnectionString("MatchesConnectionString"));
    }

    public void EnsureDatabase()
    {
        Context.Database.Migrate();
    }

    public async Task ResetState()
    {
        if (await Context.Database.CanConnectAsync())
        {
            await Context.Courts.ExecuteDeleteAsync();
            await Context.Reservations.ExecuteDeleteAsync();
            await Context.Matches.ExecuteDeleteAsync();
        }
    }

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
