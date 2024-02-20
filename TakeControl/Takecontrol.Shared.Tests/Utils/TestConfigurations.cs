using Microsoft.Extensions.Configuration;

namespace Takecontrol.Shared.Tests.Utils;

public static class TestConfigurations
{
    private static readonly string ApiName = "Takecontrol.API";
    private static readonly string ConfigName = $"appsettings.Testing.json";

    public static IConfiguration GetAppTestingConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName, ApiName);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(ConfigName, true)
                .AddEnvironmentVariables();

        return builder.Build();
    }

    public static string GetConnectionString(string connectionString)
    {
        return GetAppTestingConfiguration().GetConnectionString(connectionString)!;
    }
}