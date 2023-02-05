using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.Infrastructure.Repositories.Primitives;

namespace takecontrol.EmailEngine.IntegrationTests.Mocks;

public class MockDbContext
{
    private static string API_NAME = "takecontrol.API";

    public static EmailDbContext GetEmailDbContext()
    {
        var options = new DbContextOptionsBuilder<EmailDbContext>()
       .UseNpgsql(GetAppConfiguration().GetConnectionString("EmailConnectionString")).Options;

        return new EmailDbContext(options);
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
}
