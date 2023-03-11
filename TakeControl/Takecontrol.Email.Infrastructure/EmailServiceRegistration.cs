using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.Emails.Application.Contracts.Emails;
using Takecontrol.Emails.Application.Contracts.Persitence.Emails;
using Takecontrol.Emails.Application.Contracts.Persitence.Templates;
using Takecontrol.Emails.Application.Contracts.Templates;
using Takecontrol.Emails.Domain.Models.Emails.Options;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Repositories.Emails;
using Takecontrol.Emails.Infrastructure.Repositories.Primitives;
using Takecontrol.Emails.Infrastructure.Repositories.Services;
using Takecontrol.Emails.Infrastructure.Repositories.Templates;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;

namespace Takecontrol.Emails.Infrastructure;

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
