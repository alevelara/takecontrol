using System.Reflection;
using Mapster;
using MapsterMapper;

namespace takecontrol.API.Mappings;

public static class MappingServiceRegistration
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;
        TypeAdapterConfig.GlobalSettings.RequireDestinationMemberSource = true;
        TypeAdapterConfig.GlobalSettings.Compile();
        return services;
    }
}
