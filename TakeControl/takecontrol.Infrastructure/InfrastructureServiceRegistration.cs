using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using takecontrol.Application.Contracts.Logger;
using takecontrol.Infrastructure.Services.Logger;

namespace takecontrol.Identity;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddTransient<ILog, Logger>();

        return service;
    }
}
