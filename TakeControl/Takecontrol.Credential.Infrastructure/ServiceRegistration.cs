using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Options;
using Takecontrol.Credential.Infrastructure.Contexts;
using Takecontrol.Credential.Infrastructure.Models;
using Takecontrol.Credential.Infrastructure.Services;

namespace Takecontrol.Credential.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureCredentialInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<TakeControlIdentityDbContext>(options
            => options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString")));
        service.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        service.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
           .AddEntityFrameworkStores<TakeControlIdentityDbContext>()
           .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

        service.AddTransient<IAuthService, AuthService>();
        service.AddAuthorization();
        service.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });

        return service;
    }
}
