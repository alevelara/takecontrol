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

public class TestBase
{
    private readonly CustomWebApplicationFactory<Program> _apiWebApplicationFactory;
    private readonly HttpClient _httpClient;

    public TestBase(CustomWebApplicationFactory<Program> apiWebApplicationFactory, HttpClient httpClient)
    {
        _apiWebApplicationFactory = apiWebApplicationFactory;
        _httpClient = httpClient;
        EnsureDatabase();
    }


    /// <summary>
    /// Crea un usuario de prueba según los parámetros
    /// </summary>
    /// <returns></returns>
    public async Task<HttpClient> CreateTestForLoginUser(string userName, string email, string password, string[] roles)
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var newUser = new ApplicationUser
        {
            UserName = userName,
            Email = email,
            UserType = UserType.Administrator
        };

        await userManager.CreateAsync(newUser, password);

        foreach (var role in roles)
        {
            await userManager.AddToRoleAsync(newUser, role);
        }

        return _httpClient;
    }

    /// <summary>
    /// Crea un usuario de prueba según los parámetros
    /// </summary>
    /// <returns></returns>
    public async Task<HttpClient> CreateTestUser(string userName, string email, string password, string[] roles)
    {
        var client = await CreateTestForLoginUser(userName, email, password, roles);

        var accessToken = await GetAccessToken(email, password);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return client;
    }


    /// <summary>
    /// Al terminar cada prueba, se resetea la base de datos
    /// </summary>
    /// <returns></returns>
    /// 
    public void Dispose()
    {
        ResetState();
        ResetIdentityState();
    }

    public void DisposeIdentity()
    {
        ResetIdentityState();
    }

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario Admin
    /// </summary>
    public Task<HttpClient> RegisterUserAsAdminAsync() =>
        CreateTestForLoginUser("admintest", "test@admin.com", "Password123!", new string[] { "Administrator" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario Admin
    /// </summary>
    public Task<HttpClient> RegisterSecuredUserAsAdmin() =>
        CreateTestForLoginUser("adminsecuredtest", "test@admin.com", "Password123!", new string[] { "Administrator" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<HttpClient> RegisterUserAsPlayerAsync() =>
        CreateTestForLoginUser("playertest", "test@player.com", "Password123!", new string[] { "Player" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<HttpClient> RegisterSecuredUserAsPlayerAsync() =>
        CreateTestForLoginUser("playersecuredtest", "test@player.com", "Password123!", new string[] { "Player" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<HttpClient> RegisterUserAsClubAsync() =>
        CreateTestForLoginUser("clubtest", "test@player.com", "Password123!", new string[] { "Club" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default
    /// </summary>
    public Task<HttpClient> RegisterSecuredUserAsClubAsync() =>
        CreateTestForLoginUser("clubsecuredtest", "test@player.com", "Password123!", new string[] { "Club" });


    /// <summary>
    /// Shortcut para ejecutar IRequests con el Mediador
    /// </summary>
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    /// <summary>
    /// Shortcut para agregar Entities a la BD
    /// </summary>
    protected async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();

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
        using var scope = _apiWebApplicationFactory.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    /// <summary>
    /// Shortcut para buscar entities según un criterio
    /// </summary>
    protected async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();

        var context = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Se asegura de crear la BD
    /// </summary>
    private void EnsureDatabase()
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TakeControlDbContext>();

        context.Database.EnsureCreated();

        EnsureIdentityDatabase();
    }

    private void EnsureIdentityDatabase()
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();
        var identityContext = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();

        identityContext.Database.EnsureCreated();
    }

    /// <summary>
    /// Shortcut para autenticar un usuario para pruebas
    /// </summary>
    private async Task<string> GetAccessToken(string email, string password)
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();

        var result = await SendAsync(new LoginQuery(email, password));

        return result.Token;
    }

    public void ResetIdentityState()
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();
        var identityContext = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();
        identityContext.Users.RemoveRange(identityContext.Users);
        identityContext.UserRoles.RemoveRange(identityContext.UserRoles);
        identityContext.SaveChanges();
    }

    public void ResetState()
    {
        using var scope = _apiWebApplicationFactory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TakeControlDbContext>();
        context.Clubs.RemoveRange(context.Clubs);
        context.Addresses.RemoveRange(context.Addresses);
        context.SaveChanges();

        var identityContext = scope.ServiceProvider.GetService<TakeControlIdentityDbContext>();
        identityContext.Users.RemoveRange(identityContext.Users);
        identityContext.SaveChanges();
        // context.SaveChanges();
    }
}