using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Credential.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Shared.Tests;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.API.Tests;

public static class APITestIntegrationServiceRegistration
{
    public static IServiceCollection AddIntegrationTestsServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dbIdentityContext = services.SingleOrDefault(
              d => d.ServiceType == typeof(DbContextOptions<TakeControlIdentityDbContext>));

        services.Remove(dbIdentityContext!);

        var dbMainContext = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<TakeControlDbContext>));
        services.Remove(dbMainContext!);

        var dbEmailContext = services.SingleOrDefault(
           d => d.ServiceType == typeof(DbContextOptions<EmailDbContext>));
        services.Remove(dbEmailContext!);

        var dbMatchContext = services.SingleOrDefault(
          d => d.ServiceType == typeof(DbContextOptions<MatchesDbContext>));
        services.Remove(dbMatchContext!);

        services.AddDbContext<TakeControlIdentityDbContext>((container, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString"));
        });

        services.AddDbContext<TakeControlDbContext>((container, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ConnectionString"));
        });

        services.AddDbContext<EmailDbContext>((container, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("EmailConnectionString"));
        });

        services.AddDbContext<MatchesDbContext>((container, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MatchesConnectionString"));
        });

        return services;
    }
}
