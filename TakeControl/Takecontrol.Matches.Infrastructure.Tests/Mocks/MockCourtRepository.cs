using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Tests.Data;

namespace Takecontrol.Matches.Infrastructure.Tests.Mocks;

public static class MockCourtRepository
{
    public static async Task<Court> AddCourt(MatchesDbContext context)
    {
        var court = CourtGenerator.CreateCourt();
        await context.Courts!.AddAsync(court);
        await context.SaveChangesAsync();

        return court;
    }
}
