using Microsoft.EntityFrameworkCore;
using Takecontrol.Credential.Infrastructure.Contexts;
using Takecontrol.Shared.Tests.Contracts;
using Takecontrol.Shared.Tests.Utils;
using static Takecontrol.Credential.Infrastructure.Contexts.TakeControlIdentityDbContext;

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
