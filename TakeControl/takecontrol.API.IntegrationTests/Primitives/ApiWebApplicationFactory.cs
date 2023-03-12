using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using takecontrol.IntegrationTest.Shared.Utils;

namespace takecontrol.API.IntegrationTests.Primitives;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    public HttpClient HttpClient { get; private set; } = default!;

    public TakeControlDb TakecontrolDb { get; private set; } = default!;

    public TakeControlIdentityDb TakeControlIdentityDb { get; private set; } = default!;

    public TakeControlEmailDb TakeControlEmailDb = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureTestServices(services =>
        {
            services.AddIntegrationTestsServices(GetAppConfiguration());
        });
    }

    public async Task InitializeAsync()
    {
        TakecontrolDb = new TakeControlDb();
        TakeControlIdentityDb = new TakeControlIdentityDb();
        TakeControlEmailDb = new TakeControlEmailDb();
        HttpClient = CreateClient();
        TakecontrolDb.EnsureDatabase();
        TakeControlIdentityDb.EnsureDatabase();
        TakeControlEmailDb.EnsureDatabase();
    }

    public new Task DisposeAsync() => Task.CompletedTask;

    private IConfiguration GetAppConfiguration()
    {
        return TestConfigurations.GetAppTestingConfiguration();
    }
}