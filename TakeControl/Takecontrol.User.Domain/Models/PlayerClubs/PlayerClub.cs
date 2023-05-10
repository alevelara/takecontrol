using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Takecontrol.Shared.Domain.Primitives;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Domain.Models.PlayerClubs.ValueObjects;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Domain.Models.PlayerClubs;

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

    public static Boolean RemovePlayerByClubIdAndPlayerId(Guid playerId, Guid clubId)
    {
        PlayerClub playerClub = new PlayerClub(playerId, clubId);

        

        // var playerClub = PlayerClubs
        //     .Where(b => b.ClubId == clubId && b.PlayerId == playerId)
        //     .FirstOrDefaultAsync();

        // if (playerClub == null) {
        //     return false;
        // }else {
        //     _dbContext.PlayerClubs.Remove(playerClub);
        //     await _dbContext.SaveChangesAsync();
        //     return true;
        // }
    }
}
