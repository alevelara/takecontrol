using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations.ValueObjects;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Reservations;

public class Reservation : BaseDomainModel
{
    public Guid Id { get; private set; }
    public Guid CourtId { get; private set; }
    public DateOnly ReservationDate { get; set; }
    public TimeOnly StartDate { get; private set; }
    public TimeOnly EndDate { get; private set; }
    public bool IsAvailable { get; private set; }

    public virtual Court Court { get; private set; }

    private Reservation(Guid courtId, TimeOnly startDate, TimeOnly endDate, DateOnly reservationDate)
    {
        Id = new ReservationId().Value;
        CourtId = courtId;
        StartDate = startDate;
        EndDate = endDate;
        ReservationDate = reservationDate;
        IsAvailable = true;
    }

    public static Reservation Create(Guid courtId, TimeOnly startDate, TimeOnly endDate, DateOnly reservationDate)
        => new Reservation(courtId, startDate, endDate, reservationDate);
}
