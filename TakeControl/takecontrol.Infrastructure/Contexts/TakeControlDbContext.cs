using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Identity;

public class TakeControlDbContext : DbContext
{
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Address> Address { get; set; }

    public TakeControlDbContext(DbContextOptions<TakeControlDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
        
        builder.Entity<Club>()
            .HasOne(c => c.Address)
            .WithOne(a => a.Club)
            .HasForeignKey<Club>(c => c.AddresId);

    }    
}


public class IdentityDBContextFactory : IDesignTimeDbContextFactory<TakeControlDbContext>
{
    public static string API_NAME = "takecontrol.API";
    public TakeControlDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TakeControlDbContext>();
        var config = GetAppConfiguration();
        optionsBuilder.UseNpgsql(config.GetConnectionString("ConnectionString"));

        return new TakeControlDbContext(optionsBuilder.Options);
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