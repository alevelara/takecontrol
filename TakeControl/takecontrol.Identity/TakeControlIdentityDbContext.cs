using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using takecontrol.Identity.Configuration;
using takecontrol.Identity.Models;

namespace takecontrol.Identity;

public class TakeControlIdentityDbContext : IdentityDbContext<ApplicationUser>
{

	public TakeControlIdentityDbContext(DbContextOptions<TakeControlIdentityDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
    }

}