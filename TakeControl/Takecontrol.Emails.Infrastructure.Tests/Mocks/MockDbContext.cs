using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Takecontrol.Emails.Infrastructure.Contexts;

namespace Takecontrol.Emails.Infrastructure.Tests.Mocks;

public class MockDbContext
{
    private static string apiName = "Takecontrol.API";

    public static EmailDbContext GetEmailDbContext()
    {
        var options = new DbContextOptionsBuilder<EmailDbContext>()
       .UseNpgsql(GetAppConfiguration().GetConnectionString("EmailConnectionString")).Options;

        return new EmailDbContext(options);
    }

    private static IConfiguration GetAppConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, apiName);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile($"appsettings.Testing.json", true)
                .AddEnvironmentVariables();

        return builder.Build();
    }
}
