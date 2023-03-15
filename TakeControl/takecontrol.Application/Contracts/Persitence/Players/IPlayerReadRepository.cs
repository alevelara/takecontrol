using Takecontrol.Domain.Models.Players;

namespace Takecontrol.Application.Contracts.Persitence.Players;

public interface IPlayerReadRepository
{
    Task<Player?> GetPlayerByUserId(Guid Id);
}
