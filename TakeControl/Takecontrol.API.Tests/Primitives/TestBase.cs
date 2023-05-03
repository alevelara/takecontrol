using System.Linq.Expressions;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Takecontrol.API.Tests.Helpers;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;
using Takecontrol.Credential.Infrastructure.Models;

namespace Takecontrol.API.Tests.Primitives;

public class TestBase
{
    private readonly ApiWebApplicationFactory<Program> _apiWebApplicationFactory;
    private readonly HttpClient _httpClient;

    public TestBase(ApiWebApplicationFactory<Program> apiWebApplicationFactory)
    {
        _apiWebApplicationFactory = apiWebApplicationFactory;
        _httpClient = apiWebApplicationFactory.HttpClient;
    }

    /// <summary>
    /// Crea un usuario de prueba según los parámetros.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task<HttpClient> CreateTestUser(string userName, string email, string password, string[] roles)
    {
        var client = await CreateTestForLoginUser(userName, email, password, roles);
        var user = await FindAsync<ApplicationUser>(c => c.UserName == userName);

        var accessToken = GetAccessToken(userName, email, user.Id.ToString(), roles[0]);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return client;
    }

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario Admin.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<HttpClient> RegisterUserAsAdminAsync() =>
        CreateTestForLoginUser("admintest", "test@admin.com", "Password123!", new string[] { "Administrator" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario Admin.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<HttpClient> RegisterSecuredUserAsAdmin() =>
        CreateTestUser("adminsecuredtest", "test@admin.com", "Password123!", new string[] { "Administrator" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<HttpClient> RegisterUserAsPlayerAsync() =>
        CreateTestForLoginUser("playertest", "test@player.com", "Password123!", new string[] { "Player" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<HttpClient> RegisterSecuredUserAsPlayerAsync() =>
        CreateTestUser("playersecuredtest", "test@player.com", "Password123!", new string[] { "Player" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<HttpClient> RegisterUserAsClubAsync() =>
        CreateTestForLoginUser("clubtest", "test@club.com", "Password123!", new string[] { "Club" });

    /// <summary>
    /// Crea un HttpClient incluyendo un JWT válido con usuario default.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<HttpClient> RegisterSecuredUserAsClubAsync() =>
        CreateTestUser("clubsecuredtest", "test@club.com", "Password123!", new string[] { "Club" });

    /// <summary>
    /// Shortcut para buscar entities según un criterio.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected async Task<TEntity?> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
        where TEntity : class
    {
        var context = _apiWebApplicationFactory.TakeControlIdentityDb.Context;

        return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Shortcut para autenticar un usuario para pruebas.
    /// </summary>
    private string GetAccessToken(string userName, string email, string id, string role)
    {
        return AuthTestHelper.GenerateToken(userName, email, id, role);
    }
}