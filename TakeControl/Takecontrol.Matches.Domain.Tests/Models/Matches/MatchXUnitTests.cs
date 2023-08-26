using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Shared.Tests.Constants;

namespace Takecontrol.Matches.Domain.Tests.Models.Matches;

[Trait("Category", Category.UnitTest)]
public class MatchXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewMatch_WhenAllFieldsArePopulated()
    {
        var reservationId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var match = Match.Create(reservationId, userId);

        Assert.NotNull(match);
        Assert.NotEqual(Guid.Empty, match.Id);
        Assert.Equal(reservationId, match.ReservationId);
        Assert.Equal(userId, match.UserId);
    }

    [Fact]
    public void Create_Should_ReturnNewMatch_WhenReservationIdIsEmpty()
    {
        var reservationId = Guid.Empty;
        var userId = Guid.NewGuid();

        var match = Match.Create(reservationId, userId);

        Assert.NotNull(match);
        Assert.NotEqual(Guid.Empty, match.Id);
        Assert.Equal(reservationId, match.ReservationId);
        Assert.Equal(userId, match.UserId);
    }

    [Fact]
    public void Create_Should_ReturnNewMatch_WhenUserIdIsEmpty()
    {
        var reservationId = Guid.NewGuid();
        var userId = Guid.Empty;

        var match = Match.Create(reservationId, userId);

        Assert.NotNull(match);
        Assert.NotEqual(Guid.Empty, match.Id);
        Assert.Equal(reservationId, match.ReservationId);
        Assert.Equal(userId, match.UserId);
    }
}
