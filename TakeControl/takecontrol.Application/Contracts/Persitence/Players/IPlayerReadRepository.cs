using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Contracts.Persitence.Players;

public interface IPlayerReadRepository
{
    Task<Player> GetPlayerById(Guid Id);
}