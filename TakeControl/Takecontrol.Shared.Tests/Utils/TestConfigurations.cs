using Microsoft.Extensions.Configuration;

namespace Takecontrol.Shared.Tests.Utils;

public static class TestConfigurations
{
    private static string apiName = "Takecontrol.API";
    private static string configName = $"appsettings.Testing.json";

    public static IConfiguration GetAppTestingConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName, apiName);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(configName, true)
                .AddEnvironmentVariables();

        return builder.Build();
    }

    public static string GetConnectionString(string connectionString)
    {
        return GetAppTestingConfiguration().GetConnectionString(connectionString)!;
    }
}