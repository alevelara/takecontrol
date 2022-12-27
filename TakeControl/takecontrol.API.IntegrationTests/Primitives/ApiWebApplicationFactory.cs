using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Graph;
using System.Data.Common;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
{
    public static string API_NAME = "takecontrol.API";

    public HttpClient HttpClient { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = GetAppConfiguration().GetConnectionString("ConnectionString");
        var connectionIdentityString = GetAppConfiguration().GetConnectionString("IdentityConnectionString");

        builder.ConfigureTestServices(services =>
        {
            var dbIdentityContext = services.SingleOrDefault(
              d => d.ServiceType == typeof(DbContextOptions<TakeControlIdentityDbContext>));

            services.Remove(dbIdentityContext);

            var dbMainContext = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<TakeControlDbContext>));

            services.Remove(dbMainContext);
            services.AddDbContext<TakeControlIdentityDbContext>((container, options) =>
            {
                options.UseNpgsql(connectionIdentityString);
            });

            services.AddDbContext<TakeControlDbContext>((container, options) =>
            {
                options.UseNpgsql(connectionString);
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

    public async Task InitializeAsync()
    {
        await EnsureDatabase();
        HttpClient = CreateClient();
    }

    public new Task DisposeAsync() => Task.CompletedTask;

    public async Task EnsureDatabase()
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TakeControlDbContext>();

        await context.Database.MigrateAsync();

        await EnsureIdentityDatabase();
    }

    public async Task EnsureIdentityDatabase()
    {
        using var scope = Services.CreateScope();
        var identityContext = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        await identityContext.Database.MigrateAsync();
    }

    public async Task ResetIdentityState()
    {
        using var scope = Services.CreateScope();
        var identityContext = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();
        identityContext.Users.RemoveRange(identityContext.Users);
        identityContext.UserRoles.RemoveRange(identityContext.UserRoles);
        await identityContext.SaveChangesAsync();
    }

    public async Task ResetState()
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TakeControlDbContext>();
        context.Clubs.RemoveRange(context.Clubs);
        context.Addresses.RemoveRange(context.Addresses);
        context.Players.RemoveRange(context.Players);
        await context.SaveChangesAsync();

        var identityContext = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();
        identityContext.Users.RemoveRange(identityContext.Users);
        identityContext.UserRoles.RemoveRange(identityContext.UserRoles);
        await identityContext.SaveChangesAsync();
    }
}