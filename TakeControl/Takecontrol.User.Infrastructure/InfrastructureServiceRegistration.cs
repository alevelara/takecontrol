using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.User.Infrastructure.Repositories.Primitives;
using Takecontrol.User.Infrastructure.Repositories.Primitives.Clubs;
using Takecontrol.User.Infrastructure.Repositories.Primitives.Players;

namespace Takecontrol.User.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<TakeControlDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

        service.AddScoped(typeof(IAsyncWriteRepository<>), typeof(WriteBaseRepository<>));
        service.AddScoped(typeof(IAsyncReadRepository<>), typeof(ReadBaseRepository<>));
        service.AddScoped<IClubReadRepository, ClubReadRepository>();
        service.AddScoped<IPlayerReadRepository, PlayerReadRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        return service;
    }
}