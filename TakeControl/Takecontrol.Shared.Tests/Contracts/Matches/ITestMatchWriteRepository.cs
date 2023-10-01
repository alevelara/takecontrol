using Takecontrol.Matches.Domain.Models.Matches;

namespace Takecontrol.Shared.Tests.Contracts.Matches;

public interface ITestMatchWriteRepository
{
    Task AddMatchAsync(Match match);
}
