using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Matches.Domain.Tests.Models.Reservations;

public class ReservationXUnitTests
{
    [Fact]
    public void Create_Should_ReturnNewReservation_WhenAllFieldsArePopulated()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var courtId = Guid.NewGuid();
        var startDate = DateTime.Now;
        var endDate = startDate.AddHours(1);

        // Act
        var reservation = Reservation.Create(userId, courtId, startDate, endDate);

        // Assert
        Assert.NotNull(reservation);
        Assert.IsType<Reservation>(reservation);
        Assert.Equal(userId, reservation.UserId);
        Assert.Equal(courtId, reservation.CourtId);
        Assert.Equal(startDate, reservation.StartDate);
        Assert.Equal(endDate, reservation.EndDate);
    }
}
