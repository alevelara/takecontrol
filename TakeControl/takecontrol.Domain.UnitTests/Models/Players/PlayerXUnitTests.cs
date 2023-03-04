using System.Xml.Linq;
using takecontrol.Domain.Models.Players;
using takecontrol.Domain.Models.Players.Enums;

namespace takecontrol.Domain.UnitTests.Models.Players;

[Trait("Category", "UnitTests")]
public class PlayerXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewBegginerPlayer_WhenAllFieldsArePopulated()
    {
        var userId = Guid.NewGuid();
        var name = "name";

        var player = Player.Create(userId, name, 1, 1, 1);

        Assert.NotNull(player);
        Assert.Equal((int)PlayerLevel.Begginer, player.PlayerLevel);
        Assert.Equal(player.Name, name);
        Assert.Equal(player.UserId, userId);
    }

    [Fact]
    public void Create_Should_ReturnNewMidPlayer_WhenAllFieldsArePopulated()
    {
        var userId = Guid.NewGuid();
        var name = "name";

        var player = Player.Create(userId, name, 1, 3, 2);

        Assert.NotNull(player);
        Assert.Equal((int)PlayerLevel.Mid, player.PlayerLevel);
        Assert.Equal(player.Name, name);
        Assert.Equal(player.UserId, userId);
    }

    [Fact]
    public void Create_Should_ReturnNewExpertPlayer_WhenAllFieldsArePopulated()
    {
        var userId = Guid.NewGuid();
        var name = "name";

        var player = Player.Create(userId, name, 2, 4, 4);

        Assert.NotNull(player);
        Assert.Equal((int)PlayerLevel.Expert, player.PlayerLevel);
        Assert.Equal(player.Name, name);
        Assert.Equal(player.UserId, userId);
    }

    [Fact]
    public void Create_Should_ReturnNewPlayer_WhenUserIdIsEmpty()
    {
        var userId = Guid.Empty;
        var name = "name";

        var player = Player.Create(userId, name, 2, 4, 4);

        Assert.NotNull(player);
        Assert.Equal((int)PlayerLevel.Expert, player.PlayerLevel);
        Assert.Equal(player.Name, name);
        Assert.Equal(player.UserId, userId);
    }

    [Fact]
    public void Create_Should_ReturnNewPlayer_WhenNameIsNull()
    {
        var userId = Guid.NewGuid();

        var player = Player.Create(userId, null, 2, 4, 4);

        Assert.NotNull(player);
        Assert.Equal((int)PlayerLevel.Expert, player.PlayerLevel);
        Assert.Null(player.Name);
        Assert.Equal(player.UserId, userId);
    }
}
