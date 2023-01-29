using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using takecontrol.Identity;
using takecontrol.Infrastructure.Repositories.Primitives;

namespace takecontrol.Infrastructure.IntegrationTests.Mocks
{
    public static class MockUnitOfWork
    {
        private static string API_NAME = "takecontrol.API";

        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            var options = new DbContextOptionsBuilder<TakeControlDbContext>()
           .UseNpgsql(GetAppConfiguration().GetConnectionString("ConnectionString")).Options;

            var takeControlContextFake = new TakeControlDbContext(options);
            takeControlContextFake.Database.EnsureCreated();
            CleanContextAsync(takeControlContextFake);

            var mockUnitOfWork = new Mock<UnitOfWork>(takeControlContextFake);

            return mockUnitOfWork;
        }

        private static IConfiguration GetAppConfiguration()
        {
            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, API_NAME);

            var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile($"appsettings.Testing.json", true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }

        private static void CleanContextAsync(TakeControlDbContext takeControlContextFake)
        {
            takeControlContextFake.Clubs.ExecuteDelete();
            takeControlContextFake.Addresses.ExecuteDelete();
            takeControlContextFake.Players.ExecuteDelete();
        }
    }
}
