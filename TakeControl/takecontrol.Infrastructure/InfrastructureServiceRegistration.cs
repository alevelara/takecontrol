using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace takecontrol.Identity;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
    {

        return service;
    }
}
