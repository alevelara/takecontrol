﻿using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Takecontrol.Credential.Application;

public static class ServiceRegistration
{
    public static IServiceCollection ConfigureCredentialApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}
