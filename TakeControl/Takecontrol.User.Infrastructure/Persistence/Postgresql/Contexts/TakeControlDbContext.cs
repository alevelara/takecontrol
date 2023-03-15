using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Takecontrol.Shared.Domain.Primitives;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Configurations;

namespace Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

public class TakeControlDbContext : DbContext
{
    public TakeControlDbContext(DbContextOptions<TakeControlDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "system";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "system";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ClubConfiguration());
        builder.ApplyConfiguration(new PlayerConfiguration());
        builder.ApplyConfiguration(new AddressConfiguration());
        builder.ApplyConfiguration(new PlayerClubConfiguration());
    }

    public DbSet<Club> Clubs { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Player> Players { get; set; }

    public DbSet<PlayerClub> PlayerClubs { get; set; }
}

public class TakeControlDBContextFactory : IDesignTimeDbContextFactory<TakeControlDbContext>
{
    public static string APINAME = "Takecontrol.API";

    public TakeControlDbContext CreateDbContext(string[] args)
    {
        var config = GetAppConfiguration();
        var optionsBuilder = new DbContextOptionsBuilder<TakeControlDbContext>()
            .UseNpgsql(config.GetConnectionString("ConnectionString"));

        return new TakeControlDbContext(optionsBuilder.Options);
    }

    public TakeControlDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TakeControlDbContext>()
            .UseNpgsql(connectionString);

        return new TakeControlDbContext(optionsBuilder.Options);
    }

    private static IConfiguration GetAppConfiguration()
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