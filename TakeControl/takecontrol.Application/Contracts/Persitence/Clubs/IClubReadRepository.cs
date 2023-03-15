using Takecontrol.Domain.Models.Clubs;

namespace Takecontrol.Application.Contracts.Persitence.Clubs;

public interface IClubReadRepository
{
    Task<Club?> GetClubByUserId(Guid userId);

    Task<List<Club>> GetAllClubsAsync();

    Task<Club?> GetClubByCodeAndClubId(Guid clubId, string code);
}
