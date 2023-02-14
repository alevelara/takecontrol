using takecontrol.Identity;
using takecontrol.Infrastructure.IntegrationTests.Mocks;

namespace takecontrol.Infrastructure.IntegrationTests.Repositories.Clubs;

public class ClubReadRepositoryXUnitTests
{
    private readonly TakeControlDbContext _dbContext;

    public ClubReadRepositoryXUnitTests()
    {
        _dbContext = new TakeControlDBContextFactory().CreateDbContext(new string[0]);
    }
}
