using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.EmailEngine.Persistence.Contexts;
using Takecontrol.Identity;

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

        return services;
    }
}
