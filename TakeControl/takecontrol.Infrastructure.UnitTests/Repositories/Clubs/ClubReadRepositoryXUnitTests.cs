﻿using takecontrol.API.IntegrationTests.Primitives;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Infrastructure.IntegrationTests.Mocks;
using takecontrol.Infrastructure.Repositories.Primitives.Clubs;

namespace takecontrol.Infrastructure.IntegrationTests.Repositories.Clubs;

[Trait("Category", "IntegrationTests")]
public class ClubReadRepositoryXUnitTests : IAsyncLifetime
{
    private readonly TakeControlDb _dbContext;

    public ClubReadRepositoryXUnitTests()
    {
        _dbContext = new TakeControlDb();
    }

    [Fact]
    public async Task GetAllClubsAsync_Should_Return_EmptyList_WhenAnyClubExistsInDb()
    {
        //Assert
        var readRepository = new ClubReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetAllClubsAsync();

        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllClubsAsync_Should_Return_PopulatedList_WhenClubsExistsInDb()
    {
        //Assert
        await MockClubRepository.AddClubsWithAddress(_dbContext.Context);
        var readRepository = new ClubReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetAllClubsAsync();

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.NotNull(result[0].Address);
    }

    [Fact]
    public async Task GetClubByUserId_Should_ReturnNull_WhenUserIdDoesntExist()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var readRepository = new ClubReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetClubByUserId(userId);

        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetClubByUserId_Should_ReturnAClub_WhenAClubIsRelatedToAnUserId()
    {
        //Arrange
        var userId = Guid.NewGuid();
        await MockClubRepository.AddClubWithUserId(_dbContext.Context, userId);

        var readRepository = new ClubReadRepository(_dbContext.Context);

        //Act
        var result = await readRepository.GetClubByUserId(userId);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Club>(result);
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public async Task DisposeAsync()
    {
        await _dbContext.ResetState();
    }
}