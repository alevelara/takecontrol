using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.Data.Common;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public static string API_NAME = "takecontrol.API";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            var dbIdentityContext = services.SingleOrDefault(
               d => d.ServiceType == typeof(DbContextOptions<TakeControlIdentityDbContext>));

            services.Remove(dbIdentityContext);

            var dbMainContext = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TakeControlDbContext>));

            services.Remove(dbMainContext);

            services.AddDbContext<TakeControlIdentityDbContext>((container, options) =>
            {
                options.UseNpgsql(GetAppConfiguration().GetConnectionString("IdentityConnectionString"));
            });

            services.AddDbContext<TakeControlDbContext>((container, options) =>
            {
                options.UseNpgsql(GetAppConfiguration().GetConnectionString("ConnectionString"));
            });
        });
    }

    IConfiguration GetAppConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, API_NAME);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile($"appsettings.Testing.json", true)
                .AddEnvironmentVariables();

        return builder.Build();
    }
}