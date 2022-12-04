using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using takecontrol.Application.Features.Accounts.Queries.Login;
using takecontrol.Domain.Models.ApplicationUser.Enum;
using takecontrol.Identity;
using takecontrol.Identity.Models;

namespace takecontrol.API.IntegrationTests.Primitives;

public class TestBase : IDisposable
{
    protected ApiWebApplicationFactory Application;

    public TestBase()
    {
        Application = new ApiWebApplicationFactory();
        EnsureDatabase();
    }

    /// <summary>
    /// Crea un usuario de prueba según los parámetros
    /// </summary>
    /// <returns></returns>
    public async Task<(HttpClient Client, ApplicationUser UserId)> CreateTestForLoginUser(string userName, string password, string[] roles)
    {
        using var scope = Application.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var newUser = new ApplicationUser
        {
            UserName = userName,
            UserType = UserType.Administrator
        };

        await userManager.CreateAsync(newUser, password);

        foreach (var role in roles)
        {
            await userManager.AddToRoleAsync(newUser, role);
        }
        
        var client = Application.CreateClient();        

        return (client, newUser);
    }

    /// <summary>
    /// Crea un usuario de prueba según los parámetros
    /// </summary>
    /// <returns></returns>
    public async Task<(HttpClient Client, ApplicationUser UserId)> CreateTestUser(string userName, string password, string[] roles)
    {
        var (client, user) = await CreateTestForLoginUser(userName, password, roles);
        
        var accessToken = await GetAccessToken(userName, password);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return (client, user);
    }


    /// <summary>
    /// Al terminar cada prueba, se resetea la base de datos
    /// </summary>
    /// <returns></returns>
    /// 
    public void Dispose()
    {
        ResetState().ConfigureAwait(false);
    }

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario Admin
    /// </summary>
    public Task<(HttpClient Client, ApplicationUser User)> GetClientAsAdminAsync() =>
        CreateTestForLoginUser("test@admin.com", "Password123!", new string[] { "Administrator" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario Admin
    /// </summary>
    public Task<(HttpClient Client, ApplicationUser User)> GetSecuredClientAsAdmin() =>
        CreateTestForLoginUser("test@admin.com", "Password123!", new string[] { "Administrator" });


    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<(HttpClient Client, ApplicationUser User)> GetClientAsPlayerAsync() =>
        CreateTestForLoginUser("test@player.com", "Password123!", new string[] { "Player" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<(HttpClient Client, ApplicationUser User)> GetSecuredClientAsPlayerAsync() =>
        CreateTestForLoginUser("test@player.com", "Password123!", new string[] { "Player" });


    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<(HttpClient Client, ApplicationUser User)> GetClientAsClubAsync() =>
        CreateTestForLoginUser("test@player.com", "Password123!", new string[] { "Club" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<(HttpClient Client, ApplicationUser User)> GetSecuredClientClubAsync() =>
        CreateTestForLoginUser("test@player.com", "Password123!", new string[] { "Club" });


    /// <summary>
    /// Shortcut para ejecutar IRequests con el Mediador
    /// </summary>
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = Application.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    /// <summary>
    /// Shortcut para agregar Entities a la BD
    /// </summary>
    protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = Application.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Shortcut para buscar entities por primary key
    /// </summary>
    protected async Task<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
        using var scope = Application.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    /// <summary>
    /// Shortcut para buscar entities según un criterio
    /// </summary>
    protected async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        using var scope = Application.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Se asegura de crear la BD
    /// </summary>
    private void EnsureDatabase()
    {
        using var scope = Application.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        context.Database.EnsureCreated();
    }

    /// <summary>
    /// Shortcut para autenticar un usuario para pruebas
    /// </summary>
    private async Task<string> GetAccessToken(string email, string password)
    {
        using var scope = Application.Services.CreateScope();

        var result = await SendAsync(new LoginQuery(email, password));        

        return result.Token;
    }
    /// <summary>
    /// Se asegura de limpiar la BD
    /// </summary>
    /// <returns></returns>
    private async Task ResetState()
    {
        using var scope = Application.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        

    }
}

