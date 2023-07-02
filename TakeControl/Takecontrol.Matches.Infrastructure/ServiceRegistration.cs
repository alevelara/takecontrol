using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Repositories.Matches;
using Takecontrol.Matches.Infrastructure.Repositories.Primitives;
using Takecontrol.Matches.Infrastructure.Repositories.Reservations;

namespace Takecontrol.User.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureMatchInfrastructureServSices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<MatchesDbContext>(options
            => options.UseNpgsql(
                configuration.GetConnectionString("MatchesConnectionString"),
                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        service.AddScoped(typeof(IAsyncWriteRepository<>), typeof(WriteBaseRepository<>));
        service.AddScoped(typeof(IAsyncReadRepository<>), typeof(ReadBaseRepository<>));
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped<IMatchReadRepository, MatchReadRepository>();
        service.AddScoped<IReservationReadRepository, ReservationReadRepository>();

        return service;
    }
}
