using Microsoft.EntityFrameworkCore;
using Moq;
using takecontrol.EmailEngine.IntegrationTests.Mocks;
using takecontrol.EmailEngine.Persistence.Contexts;

namespace takecontrol.EmailEngine.IntegrationTests
{
    public class EmailDbContextFixture : IAsyncLifetime
    {
        public EmailDbContext EmailDbContext { get; set; }

        public EmailDbContextFixture()
        {
            EmailDbContext = MockDbContext.GetEmailDbContext();
        }

        public async Task DisposeAsync()
        {
            await EmailDbContext.Emails.ExecuteDeleteAsync();
            await EmailDbContext.Templates.ExecuteDeleteAsync();
        }

        public async Task InitializeAsync()
        {
            await EmailDbContext.Database.MigrateAsync();
        }
    }
}
