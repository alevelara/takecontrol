using Microsoft.EntityFrameworkCore;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Tests.Mocks;
using Xunit;

namespace Takecontrol.Emails.Infrastructure.Tests
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
