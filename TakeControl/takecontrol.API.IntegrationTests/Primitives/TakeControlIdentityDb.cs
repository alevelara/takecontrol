using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Contracts;
using takecontrol.Identity;

namespace takecontrol.API.IntegrationTests.Primitives;

public class TakeControlIdentityDb : IDbConfiguration
{
    public TakeControlIdentityDbContext Context { get; private set; }

    public TakeControlIdentityDb(TakeControlIdentityDbContext context)
    {
        Context = context;
    }

    public async Task EnsureDatabase()
    {
        await this.Context.Database.MigrateAsync();
    }

    public async Task ResetState()
    {
        this.Context.Users.RemoveRange(Context.Users);
        this.Context.UserRoles.RemoveRange(Context.UserRoles);
        await Context.SaveChangesAsync();
    }
}
