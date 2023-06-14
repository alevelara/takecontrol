using DateOnlyTimeOnly.AspNet.Converters;
using Takecontrol.API;
using Takecontrol.API.Mappings;
using Takecontrol.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    builder.Services.ConfigureInfrastructureServices(builder.Configuration);
    builder.Services.ConfigureApplicationServices();
    builder.Services.AddMappings();
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
    app.UseAuthentication();
    app.UseAuthorization();
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