using Takecontrol.Domain.Models.PlayerClubs;

namespace Takecontrol.Application.Contracts.Persitence.PlayerClubs;

public interface IPlayerClubsReadRepository
{
    Task<List<PlayerClub>> GetAllPlayersByClubId(Guid ClubId);
}
