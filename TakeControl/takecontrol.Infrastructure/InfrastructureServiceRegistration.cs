using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Application.Contracts.Persitence.Clubs;
using Takecontrol.Application.Contracts.Persitence.Players;
using Takecontrol.Application.Contracts.Persitence.Primitives;
using Takecontrol.Infrastructure.Repositories.Primitives;
using Takecontrol.Infrastructure.Repositories.Primitives.Clubs;
using Takecontrol.Infrastructure.Repositories.Primitives.Players;

namespace Takecontrol.Identity;

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