using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.Reservations;

namespace Takecontrol.Matches.Application.Tests.TestData.Matches;

public static class MatchTestData
{
    public static Match CreateMatchWithReservationForTest(DateTime startDate, Guid userId)
    {
        var reservation = CreateReservationForTest(startDate);
        var match = Match.Create(reservation.Id, userId);
        match.SetReservation(reservation);
        return match;
    }

    public static Reservation CreateReservationForTest(DateTime startDate)
    {
        return Reservation.Create(Guid.NewGuid(), new TimeOnly(startDate.Hour), new TimeOnly(startDate.AddHours(2).Hour), DateOnly.FromDateTime(startDate), isAvailable: true);
    }

    public static MatchPlayer CreateMatchPlayerForTest(Guid matchId, Guid playerId)
    {
        return MatchPlayer.Create(matchId, playerId);
    }
}
