using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Application.Contracts.Emails;
using Takecontrol.Application.Contracts.Persitence.Emails;
using Takecontrol.Application.Contracts.Persitence.Primitives;
using Takecontrol.Application.Contracts.Persitence.Templates;
using Takecontrol.Application.Contracts.Templates;
using Takecontrol.Domain.Models.Emails.Options;
using Takecontrol.EmailEngine.Persistence.Contexts;
using Takecontrol.EmailEngine.Repositories.Emails;
using Takecontrol.EmailEngine.Repositories.Primitives;
using Takecontrol.EmailEngine.Repositories.Templates;
using Takecontrol.EmailEngine.Services;
using Takecontrol.Infrastructure.Repositories.Primitives;

namespace Takecontrol.EmailEngine;

public static class EmailServiceRegistration
{
    public static IServiceCollection RegisterEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmailDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

        services.AddScoped(typeof(IAsyncWriteRepository<>), typeof(WriteBaseRepository<>));
        services.AddScoped(typeof(IAsyncReadRepository<>), typeof(ReadBaseRepository<>));
        services.AddScoped<IEmailUnitOfWork, EmailUnitOfWork>();
        services.AddScoped<IEmailWriteRepository, EmailWriteRepository>();

        services.AddScoped<ITemplateAsyncReadRepository, TemplateReadRepository>();
        services.AddScoped<ITemplateLoader, TemplateLoader>();

        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}
