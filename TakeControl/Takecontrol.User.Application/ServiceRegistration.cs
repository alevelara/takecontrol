using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Takecontrol.User.Application;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureUserApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}
