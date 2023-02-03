using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace takecontrol.API.IntegrationTests.Primitives;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
{
    public static string API_NAME = "takecontrol.API";

    public HttpClient HttpClient { get; private set; } = default!;

    public TakeControlDb TakecontrolDb = default!;

    public TakeControlIdentityDb TakeControlIdentityDb = default!;

    public TakeControlEmailDb TakeControlEmailDb = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureTestServices(services =>
        {
            services.AddAPIIntegrationTestsServices(GetAppConfiguration());
        });
    }

    IConfiguration GetAppConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, API_NAME);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile($"appsettings.Testing.json", true)
                .AddEnvironmentVariables();

        return builder.Build();
    }

    public async Task InitializeAsync()
    {
        TakecontrolDb = new TakeControlDb(GetAppConfiguration().GetConnectionString("ConnectionString"));
        TakeControlIdentityDb = new TakeControlIdentityDb(GetAppConfiguration().GetConnectionString("IdentityConnectionString"));
        TakeControlEmailDb = new TakeControlEmailDb(GetAppConfiguration().GetConnectionString("EmailConnectionString"));
        HttpClient = CreateClient();
        TakecontrolDb.EnsureDatabase();
        TakeControlIdentityDb.EnsureDatabase();
        TakeControlEmailDb.EnsureDatabase();
    }

    public new Task DisposeAsync() => Task.CompletedTask;
}