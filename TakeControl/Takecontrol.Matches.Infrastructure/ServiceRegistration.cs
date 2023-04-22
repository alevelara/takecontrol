using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Repositories.Primitives;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;

namespace Takecontrol.User.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureUserInfrastructureServSices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<MatchesDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("MatchesConnectionString")));

        service.AddScoped(typeof(IAsyncWriteRepository<>), typeof(WriteBaseRepository<>));
        service.AddScoped(typeof(IAsyncReadRepository<>), typeof(ReadBaseRepository<>));
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        return service;
    }
}