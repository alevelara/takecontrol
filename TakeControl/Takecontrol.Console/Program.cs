using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Takecontrol.Console;
using Takecontrol.Console.Controllers.Credentials;
using Takecontrol.Console.Repositories.Credentials;
using Takecontrol.Console.Services.Credentials;

using IHost host = CreateHostBuilder(args).Build();
using var scope = host.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    AnsiConsole.MarkupLineInterpolated($"[underline red]Welcome to Takecontrol![/]");
    await services.GetRequiredService<App>().RunAsync(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            services.AddScoped<ICredentialService, CredentialService>();
            services.AddScoped<CredentialsRepository>();
            services.AddSingleton<App>();
            services.AddHttpClient<CredentialsRepository>();
            services.AddSingleton(context.Configuration);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddSingleton<CredentialController>();
        });
}
