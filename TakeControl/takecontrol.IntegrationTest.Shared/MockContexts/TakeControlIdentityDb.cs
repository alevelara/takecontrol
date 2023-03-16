using Microsoft.EntityFrameworkCore;
using takecontrol.Identity;
using takecontrol.IntegrationTest.Shared.Utils;
using takecontrol.IntegrationTests.Shared.Contracts;
using static takecontrol.Identity.TakeControlIdentityDbContext;

namespace takecontrol.API.IntegrationTests.Shared.MockContexts;

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
        this.Context.Database.Migrate();
        return Task.CompletedTask;
    }

    public async Task ResetState()
    {
        if (await this.Context.Database.CanConnectAsync())
        {
            await this.Context.Users.ExecuteDeleteAsync();
            await this.Context.UserRoles.ExecuteDeleteAsync();
        }
    }

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
