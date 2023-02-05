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

        public Task DisposeAsync() => Task.CompletedTask;

        public Task InitializeAsync()
        {
            EmailDbContext.Database.Migrate();
            return Task.CompletedTask;
        }
    }
}
