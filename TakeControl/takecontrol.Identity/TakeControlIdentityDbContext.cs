using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using takecontrol.Identity.Configuration;
using takecontrol.Identity.Models;

namespace takecontrol.Identity;

public class TakeControlIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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

        builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
    }

    public class IdentityDBContextFactory : IDesignTimeDbContextFactory<TakeControlIdentityDbContext>
    {
        public static string API_NAME = "takecontrol.API";
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

            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, API_NAME);

            var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}