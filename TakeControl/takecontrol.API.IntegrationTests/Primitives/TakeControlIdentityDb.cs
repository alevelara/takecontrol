﻿using Microsoft.EntityFrameworkCore;
using takecontrol.API.IntegrationTests.Contracts;
using takecontrol.Identity;
using static takecontrol.Identity.TakeControlIdentityDbContext;

namespace takecontrol.API.IntegrationTests.Primitives;

public class TakeControlIdentityDb : IDbConfiguration
{
    public TakeControlIdentityDbContext Context { get; private set; }

    public TakeControlIdentityDb(string connectionString)
    {
        var factory = new IdentityDBContextFactory();
        Context = factory.CreateDbContext(connectionString);
    }

    public void EnsureDatabase()
    {
        this.Context.Database.Migrate();
    }

    public async Task ResetState()
    {
        if (await this.Context.Database.CanConnectAsync())
        {
            await this.Context.Users.ExecuteDeleteAsync();
            await this.Context.UserRoles.ExecuteDeleteAsync();
        }
    }
}