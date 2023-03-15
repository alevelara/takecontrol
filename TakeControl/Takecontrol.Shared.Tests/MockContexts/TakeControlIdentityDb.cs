using Microsoft.EntityFrameworkCore;
using Takecontrol.Shared.Tests.Contracts;

namespace Takecontrol.Shared.Tests.MockContexts;

public class TakeControlIdentityDb : IDbConfiguration
{
    public TakeControlIdentityDbContext Context { get; private set; }

    public TakeControlIdentityDb()
    {
        var factory = new IdentityDBContextFactory();
        Context = factory.CreateDbContext(TestConfigurations.GetConnectionString("IdentityConnectionString"));
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
            await Context.Users.ExecuteDeleteAsync();
            await Context.UserRoles.ExecuteDeleteAsync();
        }
    }

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
