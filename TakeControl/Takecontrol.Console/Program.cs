// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var host = builder.Build();

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7167");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

AnsiConsole.MarkupLineInterpolated($"[underline red]Hello[/] World!");

await host.RunAsync();
