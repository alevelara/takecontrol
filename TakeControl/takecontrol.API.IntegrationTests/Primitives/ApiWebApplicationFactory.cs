using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class ApiWebApplicationFactory : WebApplicationFactory<WebAPI>
{
    public static string API_NAME = "takecontrol.API";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        base.ConfigureWebHost(builder);
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var connectionString = GetAppConfiguration().GetConnectionString("ConnectionString");
        builder.ConfigureServices(services =>
        {
            services.AddDbContext<TakeControlDbContext>(options
            => options.UseNpgsql(connectionString));
        });

        return base.CreateHost(builder);
    }

    IConfiguration GetAppConfiguration()
    {
        var environmentName =
                  Environment.GetEnvironmentVariable(
                      "Testing");

        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, API_NAME);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

        return builder.Build();
    }
}