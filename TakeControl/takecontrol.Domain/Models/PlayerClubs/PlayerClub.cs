using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Domain.Models.PlayerClubs.ValueObjects;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Domain.Primitives;

namespace Takecontrol.Domain.Models.PlayerClubs;

public class PlayerClub : BaseDomainModel
{
    public Guid Id { get; private set; }

    [Required]
    [ForeignKey("FK_PlayerId")]
    public Guid PlayerId { get; private set; }

    [Required]
    [ForeignKey("FK_ClubId")]
    public Guid ClubId { get; private set; }

    public virtual Player Player { get; private set; }
    public virtual Club Club { get; private set; }

    private PlayerClub(Guid playerId, Guid clubId)
    {
        Id = new PlayerClubId().Value;
        PlayerId = playerId;
        ClubId = clubId;
    }

    public static PlayerClub Create(Guid playerId, Guid clubId)
    {
        return new PlayerClub(playerId, clubId);
    }
}
