using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime where TProgram : class
{
    public static string API_NAME = "takecontrol.API";

    public HttpClient HttpClient { get; private set; } = default!;

    public TakeControlDb TakecontrolDb = default!;

    public TakeControlIdentityDb TakeControlIdentityDb = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
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
        TakecontrolDb = new TakeControlDb(GetTakeControlDbContext());
        TakeControlIdentityDb = new TakeControlIdentityDb(GetTakeControlIdentityDbContext());
        HttpClient = CreateClient();
        TakecontrolDb.EnsureDatabase();
        TakeControlIdentityDb.EnsureDatabase();
    }

    public new Task DisposeAsync() => Task.CompletedTask;

    private TakeControlDbContext GetTakeControlDbContext()
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetService<TakeControlDbContext>();
    }

    private TakeControlIdentityDbContext GetTakeControlIdentityDbContext()
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();
    }
}