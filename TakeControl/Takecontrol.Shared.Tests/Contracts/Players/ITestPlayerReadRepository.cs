using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.Shared.Tests.Contracts.Players;

public interface ITestPlayerReadRepository
{
    Task<Player?> GetPlayerByName(string name);
}
