using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations.ValueObjects;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Reservations;

public class Reservation : BaseDomainModel
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CourtId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public virtual Court Court { get; private set; }

    private Reservation(Guid userId, Guid courtId, DateTime startDate, DateTime endDate)
    {
        Id = new ReservationId().Value;
        UserId = userId;
        CourtId = courtId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public static Reservation Create(Guid userId, Guid courtId, DateTime startDate, DateTime endDate)
        => new Reservation(userId, courtId, startDate, endDate);
}
