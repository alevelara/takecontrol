using System.ComponentModel.DataAnnotations;
using Takecontrol.Matches.Domain.Models.Matches.ValueObjects;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Matches;

public class Match : BaseDomainModel
{
    [Required]
    public Guid Id { get; private set; }

    [Required]
    public Guid ReservationId { get; private set; }

    public bool IsClosed { get; private set; } = false;

    public virtual Reservation Reservation { get; private set; }

    private Match(Guid reservationId)
    {
        Id = new MatchId().Value;
        ReservationId = reservationId;
    }

    public static Match Create(Guid reservationId)
    {
        return new Match(reservationId);
    }
}
