using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Contracts;
using takecontrol.EmailEngine.Persistence.Contexts;
using static takecontrol.EmailEngine.Persistence.Contexts.EmailDbContext;

namespace takecontrol.API.IntegrationTests.Primitives;

public class TakeControlEmailDb : IDbConfiguration
{
    public EmailDbContext Context { get; private set; }

    public TakeControlEmailDb(string connectionString)
    {
        var factory = new EmailDBContextFactory();
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
            await this.Context.Emails.ExecuteDeleteAsync();
            await this.Context.Templates.ExecuteDeleteAsync();
        }
    }
}
