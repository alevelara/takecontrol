using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using takecontrol.Application.Contracts.Emails;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Models.Emails.Options;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.EmailEngine.Repositories.Primitives;
using takecontrol.EmailEngine.Services;
using takecontrol.Infrastructure.Repositories.Primitives;

namespace takecontrol.EmailEngine;

public static class EmailServiceRegistration
{
    public static IServiceCollection RegisterEmailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EmailDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

        services.AddScoped(typeof(IAsyncWriteRepository<>), typeof(WriteBaseRepository<>));
        services.AddScoped(typeof(IAsyncReadRepository<>), typeof(ReadBaseRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        services.AddTransient<IEmailSender, EmailSender>();
        return services;
    }
}
