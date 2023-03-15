using Microsoft.EntityFrameworkCore;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Persistence.Data;
using Takecontrol.Shared.Tests.Contracts;
using static Takecontrol.Emails.Infrastructure.Contexts.EmailDbContext;

namespace Takecontrol.Shared.Tests.MockContexts;

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
        Context.Database.Migrate();
        await SeedData();
    }

    public async Task ResetState()
    {
        if (await Context.Database.CanConnectAsync())
        {
            await Context.Emails.ExecuteDeleteAsync();
            await Context.Templates.ExecuteDeleteAsync();
        }
    }

    public async Task SeedData()
    {
        await Context.Templates.AddAsync(TemplateFactory.WelcomeTemplate);
        await Context.SaveChangesAsync();
    }
}
