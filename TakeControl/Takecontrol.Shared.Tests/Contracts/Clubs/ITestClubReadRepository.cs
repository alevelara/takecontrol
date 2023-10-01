using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.Shared.Tests.Contracts.Clubs;

public interface ITestClubReadRepository
{
    Task<Club?> GetClubByName(string name);
}