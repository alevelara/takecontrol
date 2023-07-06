using AutoFixture;
using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Matches.Infrastructure.Tests.Data;

public static class ReservationGenerator
{
    public static List<Reservation> CreateReservations(bool isAvailable)
    {
        var fixture = new Fixture();
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var reservations = fixture.CreateMany<Reservation>().ToList();
        reservations.Add(fixture.Build<Reservation>()
            .With(c => c.IsAvailable, isAvailable)
            .Without(c => c.Court)
            .Create());

        return reservations;
    }

    public static Reservation CreateReservation(bool isAvailable, Guid courtId)
    {
        return Reservation.Create(courtId, new TimeOnly(10, 0), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.Now), isAvailable);
    }
}
