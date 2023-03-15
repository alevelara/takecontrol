using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Takecontrol.API.IntegrationTests.Shared.MockContexts;
using Takecontrol.IntegrationTest.Shared.Utils;

namespace Takecontrol.API.IntegrationTests.Primitives;

public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    public HttpClient HttpClient { get; private set; } = default!;

    public TakeControlDb TakecontrolDb { get; private set; } = default!;

    public TakeControlIdentityDb TakeControlIdentityDb { get; private set; } = default!;

    public TakeControlEmailDb TakeControlEmailDb { get; private set; } = default!;

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

        await TakecontrolDb.EnsureDatabase();
        await TakeControlIdentityDb.EnsureDatabase();
        await TakeControlEmailDb.EnsureDatabase();
    }

    public new Task DisposeAsync() => Task.CompletedTask;

    private IConfiguration GetAppConfiguration()
    {
        return TestConfigurations.GetAppTestingConfiguration();
    }
}