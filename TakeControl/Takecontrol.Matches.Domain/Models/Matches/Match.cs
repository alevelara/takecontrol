using System.ComponentModel.DataAnnotations;
using Takecontrol.Matches.Domain.Models.Matches.ValueObjects;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Matches;

public class Match : BaseDomainModel
{
    [Required]
    public Guid Id { get; private set; }

    [Required]
    public Guid ReservationId { get; private set; }

    [Required]
    public Guid UserId { get; private set; }

    public bool IsClosed { get; private set; } = false;

    public bool IsCancelled { get; private set; } = false;

    public string? CancelledDescription { get; private set; };

    public virtual Reservation Reservation { get; private set; }

    public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }

    private Match(Guid reservationId, Guid userId)
    {
        Id = new MatchId().Value;
        ReservationId = reservationId;
        UserId = userId;
        IsClosed = false;
        IsCancelled = false;
        CancelledDescription = string.Empty;
    }

    public static Match Create(Guid reservationId, Guid userId)
    {
        return new Match(reservationId, userId);
    }

    public void Close()
    {
        IsClosed = true;
    }

    public void Open()
    {
        IsClosed = false;
    }

    public void Cancel(string? description = null)
    {
        IsCancelled = true;
        CancelledDescription = description;
    }
}
