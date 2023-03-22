using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Takecontrol.Emails.Application.Services.Emails;

namespace Takecontrol.Emails.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection ConfigureEmailApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient<ISendEmailService, SendEmailService>();
            return services;
        }
    }
}
