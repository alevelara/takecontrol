﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.User.Infrastructure.Repositories.Clubs;
using Takecontrol.User.Infrastructure.Repositories.PlayerClubs;
using Takecontrol.User.Infrastructure.Repositories.Players;
using Takecontrol.User.Infrastructure.Repositories.Primitives;

namespace Takecontrol.User.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureUserInfrastructureServSices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<TakeControlDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

        service.AddScoped(typeof(IAsyncWriteRepository<>), typeof(WriteBaseRepository<>));
        service.AddScoped(typeof(IAsyncReadRepository<>), typeof(ReadBaseRepository<>));
        service.AddScoped<IClubReadRepository, ClubReadRepository>();
        service.AddScoped<IPlayerReadRepository, PlayerReadRepository>();
        service.AddScoped<IPlayerClubReadRepository, PlayerClubReadRepository>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        return service;
    }
}