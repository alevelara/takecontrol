using Takecontrol.Matches.Domain.Models.Matches;

namespace Takecontrol.Matches.Domain.Tests.Models.Matches;

public class MatchXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewMatch_WhenAllFieldsArePopulated()
    {
        var reservationId = Guid.NewGuid();

        var match = Match.Create(reservationId);

        Assert.NotNull(match);
        Assert.NotEqual(Guid.Empty, match.Id);
        Assert.Equal(reservationId, match.ReservationId);
    }

    [Fact]
    public void Create_Should_ReturnNewMatch_WhenReservationIdIsEmpty()
    {
        var reservationId = Guid.Empty;

        var match = Match.Create(reservationId);

        Assert.NotNull(match);
        Assert.NotEqual(Guid.Empty, match.Id);
        Assert.Equal(reservationId, match.ReservationId);
    }
}
