using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Configurations;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;

public class MatchesDbContext : DbContext
{
    public MatchesDbContext(DbContextOptions<MatchesDbContext> options) : base(options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MatchConfiguration());
        modelBuilder.ApplyConfiguration(new CourtConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
    }

    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Court> Courts { get; set; }
}

public class MatchesDbContextFactory : IDesignTimeDbContextFactory<MatchesDbContext>
{
    public static string APINAME = "Takecontrol.API";

    public MatchesDbContext CreateDbContext(string[] args)
    {
        var config = GetAppConfiguration();
        var optionsBuilder = new DbContextOptionsBuilder<MatchesDbContext>()
            .UseNpgsql(config.GetConnectionString("MatchesConnectionString"));

        return new MatchesDbContext(optionsBuilder.Options);
    }

    public MatchesDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MatchesDbContext>()
            .UseNpgsql(connectionString);

        return new MatchesDbContext(optionsBuilder.Options);
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
