using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Matches.Infrastructure.Tests.Data;

namespace Takecontrol.Matches.Infrastructure.Tests.Mocks;

public static class MockReservationRepository
{
    public static async Task AddReservations(MatchesDbContext context)
    {
       var reservations = ReservationGenerator.CreateReservations(isAvailable: true);

       await context.Reservations!.AddRangeAsync(reservations);
       await context.SaveChangesAsync();
    }

    public static async Task AddUnavaibleReservations(MatchesDbContext context)
    {
        var reservations = ReservationGenerator.CreateReservations(isAvailable: false);

        await context.Reservations!.AddRangeAsync(reservations);
        await context.SaveChangesAsync();
    }

    public static async Task<Reservation> AddReservation(MatchesDbContext context, Guid courtId)
    {
        var reservation = ReservationGenerator.CreateReservation(isAvailable: true, courtId);

        await context.Reservations!.AddAsync(reservation);
        await context.SaveChangesAsync();

        return reservation;
    }

    public static async Task<Reservation> AddUnavaibleReservation(MatchesDbContext context, Guid courtId)
    {
        var reservation = ReservationGenerator.CreateReservation(isAvailable: false, courtId);

        await context.Reservations!.AddAsync(reservation);
        await context.SaveChangesAsync();

        return reservation;
    }
}
