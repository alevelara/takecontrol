using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Tests.Constants;

namespace Takecontrol.Matches.Domain.Tests.Models.Reservations;

[Trait("Category", Category.UnitTest)]
public class ReservationProcessXUnitTest : IClassFixture<ReservationProcessXUnitTest>
{
    [Theory]
    [InlineData("10:00", "12:00", 8)]
    [InlineData("10:00", "13:00", 16)]
    [InlineData("10:00", "13:50", 16)]
    [InlineData("10:00", "10:50", 0)]
    [InlineData("10:00", "20:30", 56)]
    [InlineData("11:00", "20:31", 48)]
    public async Task GenerateReservationsByHours_Should_CreateDifferentNumberOfReservations_ForDifferentsHours(string openDate, string closureDate, int numberOfReservations)
    {
        //Arrange
        var openTimeOnly = TimeOnly.Parse(openDate);
        var closureTimeOnly = TimeOnly.Parse(closureDate);

        //Act
        var totalReservations = ReservationProcess.GenerateReservationsByHoursInAWeek(Guid.NewGuid(), openTimeOnly, closureTimeOnly);

        //Assert
        Assert.Equal(numberOfReservations, totalReservations.Count);
    }
}
