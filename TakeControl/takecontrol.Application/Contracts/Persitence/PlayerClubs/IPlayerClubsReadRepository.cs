using takecontrol.Domain.Models.PlayerClubs;

namespace takecontrol.Application.Contracts.Persitence.PlayerClubs;

public interface IPlayerClubsReadRepository
{
    Task<List<PlayerClub>> GetAllPlayersByClubId(Guid ClubId);
}
