using Microsoft.EntityFrameworkCore;
using takecontrol.Identity;
using takecontrol.IntegrationTest.Shared.Utils;
using takecontrol.IntegrationTests.Shared.Contracts;

namespace takecontrol.API.IntegrationTests.Shared.MockContexts;

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
        this.Context.Database.Migrate();
        return Task.CompletedTask;
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

    public Task SeedData()
    {
        return Task.CompletedTask;
    }
}
