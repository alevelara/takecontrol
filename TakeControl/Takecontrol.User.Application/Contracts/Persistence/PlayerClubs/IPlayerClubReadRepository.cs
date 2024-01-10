using Takecontrol.User.Domain.Models.PlayerClubs;

namespace Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;

public interface IPlayerClubReadRepository
{
    Task<List<PlayerClub>> GetAllPlayersByClubId(Guid clubId);
    Task<PlayerClub?> GetByPlayerIdAndClubId(Guid playerId, Guid clubId);
}
