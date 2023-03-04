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
    }

    public class IdentityDBContextFactory : IDesignTimeDbContextFactory<TakeControlIdentityDbContext>
    {
        public static string APINAME = "takecontrol.API";

        public TakeControlIdentityDbContext CreateDbContext(string[] args)
        {
            var config = GetAppConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<TakeControlIdentityDbContext>()
                .UseNpgsql(config.GetConnectionString("IdentityConnectionString"));

            return new TakeControlIdentityDbContext(optionsBuilder.Options);
        }

        public TakeControlIdentityDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TakeControlIdentityDbContext>()
                .UseNpgsql(connectionString);

            return new TakeControlIdentityDbContext(optionsBuilder.Options);
        }

        private IConfiguration GetAppConfiguration()
        {
            var environmentName =
                      Environment.GetEnvironmentVariable(
                          "ASPNETCORE_ENVIRONMENT");

            var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, APINAME);

            var builder = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}