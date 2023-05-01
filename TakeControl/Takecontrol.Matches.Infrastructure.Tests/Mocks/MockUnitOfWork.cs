using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Repositories.Primitives;

namespace Takecontrol.Matches.Infrastructure.Tests.Mocks;

public class MockUnitOfWork
{
    private readonly MatchesDbContext _context;
    private const string ApiName = "Takecontrol.API";

    public MockUnitOfWork()
    {
        _context = InitFakeContext();
    }

    public Mock<UnitOfWork> GetUnitOfWork()
    {
        var mockUnitOfWork = new Mock<UnitOfWork>(_context);

        return mockUnitOfWork;
    }

    public MatchesDbContext GetContext() => _context;

    private MatchesDbContext InitFakeContext()
    {
        var options = new DbContextOptionsBuilder<MatchesDbContext>()
      .UseNpgsql(GetAppConfiguration().GetConnectionString("MatchesConnectionString")).Options;

        var matchesDbContextFake = new MatchesDbContext(options);
        matchesDbContextFake.Database.Migrate();
        CleanContextAsync(matchesDbContextFake);

        return matchesDbContextFake;
    }

    private IConfiguration GetAppConfiguration()
    {
        var path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName, ApiName);

        var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile($"appsettings.Testing.json", true)
                .AddEnvironmentVariables();

        return builder.Build();
    }

    private void CleanContextAsync(MatchesDbContext takeControlContextFake)
    {
        takeControlContextFake.Courts.ExecuteDelete();
        takeControlContextFake.Matches.ExecuteDelete();
        takeControlContextFake.Reservations.ExecuteDelete();
    }
}
