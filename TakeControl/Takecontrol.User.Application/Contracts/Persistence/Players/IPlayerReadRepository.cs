using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Contracts.Persistence.Players;

public interface IPlayerReadRepository
{
    Task<Player?> GetPlayerByUserId(Guid Id);
}
