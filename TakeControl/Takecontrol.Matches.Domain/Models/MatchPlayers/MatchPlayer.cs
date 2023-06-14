using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers.ValueObjects;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.MatchPlayers;

public class MatchPlayer : BaseDomainModel
{
    [Required]
    public Guid PlayerId { get; private set; }

    [Required]
    [ForeignKey("FK_MatchId")]
    public Guid MatchId { get; private set; }

    public virtual Match Match { get; private set; }

    private MatchPlayer(Guid playerId)
    {
        MatchId = new MatchPlayerId().Value;
        PlayerId = playerId;
    }

    public static MatchPlayer Create(Guid playerId) => new MatchPlayer(playerId);
}
