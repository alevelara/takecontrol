// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Takecontrol.Console.Repositories.Credentials;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var host = builder.Build();

builder.Services.AddHttpClient<CredentialsRepository>();

AnsiConsole.MarkupLineInterpolated($"[underline red]Hello[/] World!");

await host.RunAsync();
