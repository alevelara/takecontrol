﻿using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Xunit;

namespace Takecontrol.User.Domain.Tests.Models.PlayerClubs;

[Trait("Category", Category.UnitTest)]
public class PlayerClubXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewPlayerClub_WhenRelationExists()
    {
        //Arrange
        var playerId = Guid.NewGuid();
        var clubId = Guid.NewGuid();

        //Act
        var result = PlayerClub.Create(playerId, clubId);

        //Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public void Create_Should_ReturnNewPlayerClub_WhenClubDoesntExist()
    {
        //Arrange
        var playerId = Guid.NewGuid();
        var clubId = Guid.Empty;

        //Act
        var result = PlayerClub.Create(playerId, clubId);

        //Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(Guid.Empty, result.ClubId);
    }

    [Fact]
    public void Create_Should_ReturnNewPlayerClub_WhenPlayerDoesntExist()
    {
        //Arrange
        var playerId = Guid.Empty;
        var clubId = Guid.NewGuid();

        //Act
        var result = PlayerClub.Create(playerId, clubId);

        //Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(Guid.Empty, result.PlayerId);
    }
}
