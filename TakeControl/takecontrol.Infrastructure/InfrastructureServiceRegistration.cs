using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using takecontrol.Application.Contracts.Persitence.Clubs;
using takecontrol.Application.Contracts.Persitence.Players;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.Infrastructure.Repositories.Primitives;
using takecontrol.Infrastructure.Repositories.Primitives.Clubs;
using takecontrol.Infrastructure.Repositories.Primitives.Players;

namespace takecontrol.Identity;

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