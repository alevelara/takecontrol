using Microsoft.EntityFrameworkCore;
using Takecontrol.EmailEngine.Persistence.Contexts;
using Takecontrol.EmailEngine.Persistence.Data;
using Takecontrol.IntegrationTest.Shared.Utils;
using Takecontrol.IntegrationTests.Shared.Contracts;
using static Takecontrol.EmailEngine.Persistence.Contexts.EmailDbContext;

namespace Takecontrol.API.IntegrationTests.Shared.MockContexts;

public class TakeControlEmailDb : IDbConfiguration
{
    public EmailDbContext Context { get; private set; }

    public TakeControlEmailDb()
    {
        var factory = new EmailDBContextFactory();
        Context = factory.CreateDbContext(TestConfigurations.GetConnectionString("EmailConnectionString"));
    }

    public async Task EnsureDatabase()
    {
        this.Context.Database.Migrate();
        await this.SeedData();
    }

    public async Task ResetState()
    {
        if (await this.Context.Database.CanConnectAsync())
        {
            await this.Context.Emails.ExecuteDeleteAsync();
            await this.Context.Templates.ExecuteDeleteAsync();
        }
    }

    public async Task SeedData()
    {
        await this.Context.Templates.AddAsync(TemplateFactory.WelcomeTemplate);
        await this.Context.SaveChangesAsync();
    }
}
