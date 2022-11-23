using takecontrol.Application;
using takecontrol.Identity;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();    
    builder.Services.ConfigureIdentityServices(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
    });
}

var app = builder.Build();
{
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
