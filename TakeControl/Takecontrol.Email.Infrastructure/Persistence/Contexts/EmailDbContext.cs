using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Infrastructure.Persistence.Configurations;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Emails.Infrastructure.Contexts;

public class EmailDbContext : DbContext
{
    public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
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
        builder.ApplyConfiguration(new TemplateConfiguration());
        builder.ApplyConfiguration(new EmailConfiguration());
    }

    public DbSet<Email> Emails { get; set; }

    public DbSet<Template> Templates { get; set; }

    public class EmailDBContextFactory : IDesignTimeDbContextFactory<EmailDbContext>
    {
        public static string APINAME = "Takecontrol.API";

        public EmailDbContext CreateDbContext(string[] args)
        {
            var config = GetAppConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<EmailDbContext>()
                .UseNpgsql(config.GetConnectionString("EmailConnectionString"));

            return new EmailDbContext(optionsBuilder.Options);
        }

        public EmailDbContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmailDbContext>()
                .UseNpgsql(connectionString);

            return new EmailDbContext(optionsBuilder.Options);
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
}
