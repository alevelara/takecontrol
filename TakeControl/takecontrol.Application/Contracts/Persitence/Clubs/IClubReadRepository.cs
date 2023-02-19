using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Contracts.Persitence.Clubs;

public interface IClubReadRepository
{
    Task<Club> GetClubByUserId(Guid userId);

    Task<List<Club>> GetAllClubsAsync();
}
