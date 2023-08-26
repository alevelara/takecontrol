using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Takecontrol.Shared.Tests.Factories;
using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.Shared.Tests.Utils;
using Xunit;

namespace Takecontrol.API.Tests.Primitives;

public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class
{
    public HttpClient HttpClient { get; private set; } = default!;

    public TakeControlDb TakecontrolDb { get; private set; } = default!;

    public TakeControlIdentityDb TakeControlIdentityDb { get; private set; } = default!;

    public TakeControlEmailDb TakeControlEmailDb { get; private set; } = default!;

    public TakeControlMatchesDb TakeControlMatchesDb { get; private set; } = default!;

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
        TakecontrolDb = DbContextFactory.CreateTakeControlDbContext();
        TakeControlIdentityDb = DbContextFactory.CreateTakeControlIdentityDbContext();
        TakeControlEmailDb = DbContextFactory.CreateTakeControlEmailDbContext();
        TakeControlMatchesDb = DbContextFactory.CreateTakeControlMatchDbContext();
        HttpClient = CreateClient();

        TakecontrolDb.EnsureDatabase();
        TakeControlIdentityDb.EnsureDatabase();
        TakeControlEmailDb.EnsureDatabase();
        TakeControlMatchesDb.EnsureDatabase();
    }

    public new Task DisposeAsync() => Task.CompletedTask;

    private IConfiguration GetAppConfiguration()
    {
        return TestConfigurations.GetAppTestingConfiguration();
    }
}