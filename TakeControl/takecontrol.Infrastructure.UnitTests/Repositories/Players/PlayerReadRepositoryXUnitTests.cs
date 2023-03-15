using Takecontrol.API.IntegrationTests.Shared.MockContexts;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Domain.Models.Players.Enums;
using Takecontrol.Infrastructure.IntegrationTests.Mocks;
using Takecontrol.Infrastructure.Repositories.Primitives.Players;

namespace Takecontrol.Infrastructure.IntegrationTests.Repositories.Players;

[Trait("Category", "IntegrationTests")]
[Collection(SharedTestCollection.Name)]
public class ClubReadRepositoryXUnitTests : IAsyncLifetime
{
    private readonly TakeControlDb _dbContext;

    public ClubReadRepositoryXUnitTests()
    {
        _dbContext = new TakeControlDb();
    }

    [Fact]
    public async Task GetPlayerById_Should_ReturnNull_WhenIdDoesntExist()
    { 
        //Arrange
        var userId = Guid.NewGuid();
        var readRepository = new PlayerReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetPlayerByUserId(userId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetPlayerById_Should_ReturnAPlayer_WhenAPlayerIsRelatedToAnId()
    {
        //Arrange
        var userId = Guid.NewGuid();
        await MockPlayerRepository.AddPlayerWithUserId(_dbContext.Context, userId);
        var readRepository = new PlayerReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetPlayerByUserId(userId);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Player>(result);
    }

    [Fact]
    public async Task GetPlayerById_Should_ReturnABeginnerPlayer()
    {
        //Arrange
        var userId = Guid.NewGuid();
        await MockPlayerRepository.AddPlayerBeginner(_dbContext.Context, userId);

        var readRepository = new PlayerReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetPlayerByUserId(userId);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Player>(result);
        Assert.Equal((int)PlayerLevel.Begginer, result.PlayerLevel);
    }

    [Fact]
    public async Task GetPlayerById_Should_ReturnAMidPlayer()
    {
        //Arrange
        var userId = Guid.NewGuid();
        await MockPlayerRepository.AddPlayerMid(_dbContext.Context, userId);

        var readRepository = new PlayerReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetPlayerByUserId(userId);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Player>(result);
        Assert.Equal((int)PlayerLevel.Mid, result.PlayerLevel);
    }

    [Fact]
    public async Task GetPlayerById_Should_ReturnAnExpertPlayer()
    {
        //Arrange
        var userId = Guid.NewGuid();
        await MockPlayerRepository.AddPlayerExpert(_dbContext.Context, userId);

        var readRepository = new PlayerReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetPlayerByUserId(userId);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Player>(result);
        Assert.Equal((int)PlayerLevel.Expert, result.PlayerLevel);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _dbContext.ResetState();
    }
}
