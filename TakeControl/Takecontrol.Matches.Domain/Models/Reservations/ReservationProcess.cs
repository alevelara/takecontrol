namespace Takecontrol.Matches.Domain.Models.Reservations;

public static class ReservationProcess
{
    private const int MATCH_DURATION = 90;
    private const int DAYS_IN_A_WEEK = 7;

    public static List<Reservation> GenerateReservationsByHoursInAWeek(Guid courtId, TimeOnly openTime, TimeOnly closureTime)
    {
        var reservations = new List<Reservation>();
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);
        var numberOfReservationsByCourt = GetNumberOfCourtsFromOpenToCloseDate(openTime, closureTime);

        for (int days = 0; days <= DAYS_IN_A_WEEK; days++)
        {
            var reservationTime = openTime;
            for (int reservation = 0; reservation < numberOfReservationsByCourt; reservation++)
            {
                reservations.Add(Reservation.Create(courtId, reservationTime, reservationTime.AddMinutes(90), date.AddDays(days)));
                reservationTime = reservationTime.AddMinutes(90);
            }
        }

        return reservations;
    }

    private static int GetNumberOfCourtsFromOpenToCloseDate(TimeOnly openTime, TimeOnly closureTime)
    {
        var minutesBetweenHours = (closureTime - openTime).TotalMinutes;
        return (int)(minutesBetweenHours / MATCH_DURATION);
    }
}
