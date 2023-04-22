using Takecontrol.Shared.Tests.MockContexts;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;
using Takecontrol.User.Domain.Models.Players.Enums;
using Takecontrol.User.Infrastructure.Repositories.PlayerClubs;
using Takecontrol.User.Infrastructure.Repositories.Players;
using Takecontrol.User.Infrastructure.Tests;
using Takecontrol.User.Infrastructure.Tests.Mocks;
using Xunit;

namespace Takecontrol.Infrastructure.IntegrationTests.Repositories.Players;

[Trait("Category", "IntegrationTests")]
[Collection(SharedTestCollection.Name)]
public class PlayerReadRepositoryXUnitTests : IAsyncLifetime
{
    private readonly TakeControlDb _dbContext;

    public PlayerReadRepositoryXUnitTests()
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

    [Fact]
    public async Task GetAllPlayersByClubId_Should_ReturnAListOfPlayers()
    {
        //Arrange
        var clubIdA = Guid.NewGuid();
        var clubIdB = Guid.NewGuid();
        var playerId1 = Guid.NewGuid();
        var playerId2 = Guid.NewGuid();
        var playerId3 = Guid.NewGuid();

        // Create players
        await MockPlayerRepository.AddPlayerExpert(_dbContext.Context, playerId1);
        await MockPlayerRepository.AddPlayerMid(_dbContext.Context, playerId2);
        await MockPlayerRepository.AddPlayerBeginner(_dbContext.Context, playerId3);

        // Create clubs
        var guidClubA = await MockClubRepository.AddClubWithUserId(_dbContext.Context, clubIdA);
        var guidClubB = await MockClubRepository.AddClubWithUserId(_dbContext.Context, clubIdB);

        // Assign players to club
        await MockPlayerRepository.AssignPlayerToClub(_dbContext.Context, clubIdA, playerId1);
        await MockPlayerRepository.AssignPlayerToClub(_dbContext.Context, clubIdA, playerId2);
        await MockPlayerRepository.AssignPlayerToClub(_dbContext.Context, clubIdB, playerId3);

        var readRepository = new PlayerClubReadRepository(_dbContext.Context);

        //Act
        var resultPlayersClubA = await readRepository.GetAllPlayersByClubId(guidClubA);
        var resultPlayersClubB = await readRepository.GetAllPlayersByClubId(guidClubB);

        //Assert
        Assert.NotNull(resultPlayersClubA);
        Assert.IsType<List<PlayerClub>>(resultPlayersClubA);
        Assert.Equal(2, resultPlayersClubA.Count);

        Assert.NotNull(resultPlayersClubB);
        Assert.IsType<List<PlayerClub>>(resultPlayersClubB);
        Assert.Equal(1, resultPlayersClubB.Count);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _dbContext.ResetState();
    }
}
