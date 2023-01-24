using takecontrol.API.Middlewares;
using takecontrol.Application;
using takecontrol.EmailEngine;
using takecontrol.Identity;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.ConfigureIdentityServices(builder.Configuration);
    builder.Services.RegisterEmailServices(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.ConfigureInfrastructureServices(builder.Configuration);
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    });
}

var app = builder.Build();
{
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.UseAuthentication();
    app.UseCors("CorsPolicy");
    app.MapControllers();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}

public partial class Program { }