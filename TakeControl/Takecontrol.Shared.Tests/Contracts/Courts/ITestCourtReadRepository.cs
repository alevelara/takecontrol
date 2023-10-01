using Takecontrol.Matches.Domain.Models.Courts;

namespace Takecontrol.Shared.Tests.Contracts.Courts;

public interface ITestCourtReadRepository
{
    Task<Court?> GetCourtByClubAsync(Guid clubId);
}
