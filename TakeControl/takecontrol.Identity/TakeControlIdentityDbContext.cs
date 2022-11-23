using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
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
            var config = GetAppConfiguration();
            optionsBuilder.UseNpgsql(config.GetConnectionString("IdentityConnectionString"));

            return new TakeControlIdentityDbContext(optionsBuilder.Options);
        }
        IConfiguration GetAppConfiguration()
        {
            var environmentName =
                      Environment.GetEnvironmentVariable(
                          "ASPNETCORE_ENVIRONMENT");

            var path = "E:\\Proyectos\\takecontrol\\TakeControl\\takecontrol.API";

            var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}