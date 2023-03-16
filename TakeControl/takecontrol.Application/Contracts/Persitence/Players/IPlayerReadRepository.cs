using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Contracts.Persitence.Players;

public interface IPlayerReadRepository
{
    Task<Player?> GetPlayerByUserId(Guid Id);

    Task<List<Player>> GetAllPlayersByClubId(Guid Id);
}
