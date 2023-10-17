using System.ComponentModel;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations.ValueObjects;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Reservations;

public class Reservation : BaseDomainModel
{
    public Guid Id { get; private set; }
    public Guid CourtId { get; private set; }
    public DateOnly ReservationDate { get; private set; }
    public TimeOnly StartDate { get; private set; }
    public TimeOnly EndDate { get; private set; }
    [DefaultValue(true)]
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

    private Reservation(Guid courtId, TimeOnly startDate, TimeOnly endDate, DateOnly reservationDate, bool isAvailable)
    {
        Id = new ReservationId().Value;
        CourtId = courtId;
        StartDate = startDate;
        EndDate = endDate;
        ReservationDate = reservationDate;
        IsAvailable = isAvailable;
    }

    public static Reservation Create(Guid courtId, TimeOnly startDate, TimeOnly endDate, DateOnly reservationDate)
        => new Reservation(courtId, startDate, endDate, reservationDate);

    public static Reservation Create(Guid courtId, TimeOnly startDate, TimeOnly endDate, DateOnly reservationDate, bool isAvailable)
        => new Reservation(courtId, startDate, endDate, reservationDate, isAvailable);

    public void SetIsAvailable(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }

    public void SetCourt(Court court)
    {
        Court = court;
    }
}
