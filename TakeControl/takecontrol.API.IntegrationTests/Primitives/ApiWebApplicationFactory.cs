using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using takecontrol.API.Controllers;

namespace takecontrol.API.IntegrationTests.Primitives;

public class ApiWebApplicationFactory : WebApplicationFactory<WebAPI>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        base.ConfigureWebHost(builder);
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

        });

        return base.CreateHost(builder);
    }
}