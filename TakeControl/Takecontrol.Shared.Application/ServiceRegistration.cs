using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Shared.Application.Behaviors;

namespace Takecontrol.Shared.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureSharedApplicationServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
