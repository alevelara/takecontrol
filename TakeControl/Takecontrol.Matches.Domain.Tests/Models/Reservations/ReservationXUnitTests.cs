using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Tests.Constants;

namespace Takecontrol.Matches.Domain.Tests.Models.Reservations;

[Trait("Category", Category.UnitTest)]
public class ReservationXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewReservation_WhenAllFieldsArePopulated()
    {
        // Arrange
        var courtId = Guid.NewGuid();
        var startTime = TimeOnly.Parse("10:00");
        var endTime = startTime.AddHours(1);
        var reservationDate = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var reservation = Reservation.Create(courtId, startTime, endTime, reservationDate);

        // Assert
        Assert.NotNull(reservation);
        Assert.IsType<Reservation>(reservation);
        Assert.Equal(courtId, reservation.CourtId);
        Assert.Equal(startTime, reservation.StartDate);
        Assert.Equal(endTime, reservation.EndDate);
    }
}
