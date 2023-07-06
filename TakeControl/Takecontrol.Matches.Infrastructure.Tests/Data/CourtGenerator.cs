using Takecontrol.Matches.Domain.Models.Courts;

namespace Takecontrol.Matches.Infrastructure.Tests.Data;

public static class CourtGenerator
{
    public static Court CreateCourt()
    {
        return Court.Create(Guid.NewGuid(), "Test 1");
    }
}
