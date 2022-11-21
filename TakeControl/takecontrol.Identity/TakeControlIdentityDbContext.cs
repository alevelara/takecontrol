using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
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

    public class IdentityDBContextFactory : IDesignTimeDbContextFactory<TakeControlIdentityDbContext>
    {
        public TakeControlIdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TakeControlIdentityDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=TakeControl;Integrated Security=true;Username=postgres");

            return new TakeControlIdentityDbContext(optionsBuilder.Options);
        }
    }

}