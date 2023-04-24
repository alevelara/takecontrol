using Takecontrol.Credential.Application;
using Takecontrol.Credential.Infrastructure;
using Takecontrol.Emails.Application;
using Takecontrol.Emails.Infrastructure;
using Takecontrol.Shared.Application;
using Takecontrol.User.Application;
using Takecontrol.User.Infrastructure;

namespace Takecontrol.API;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureUserInfrastructureServSices(configuration);
        services.ConfigureEmailInfrastructureServices(configuration);
        services.ConfigureCredentialInfrastructureServices(configuration);
        services.ConfigureMatchInfrastructureServSices(configuration);
        return services;
    }

    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.ConfigureUserApplicationServices();
        services.ConfigureEmailApplicationServices();
        services.ConfigureCredentialApplicationServices();
        services.ConfigureSharedApplicationServices();
        return services;
    }
}
