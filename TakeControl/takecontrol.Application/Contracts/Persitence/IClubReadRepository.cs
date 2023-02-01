using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Contracts.Persitence;

public interface IClubReadRepository
{
    Task<Club> GetClubById(Guid id);
}
