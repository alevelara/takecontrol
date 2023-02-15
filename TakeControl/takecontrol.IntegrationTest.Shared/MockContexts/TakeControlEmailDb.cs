using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Contracts;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.EmailEngine.Persistence.Data;
using takecontrol.IntegrationTest.Shared.Utils;
using static takecontrol.EmailEngine.Persistence.Contexts.EmailDbContext;

namespace takecontrol.API.IntegrationTests.Primitives;

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
