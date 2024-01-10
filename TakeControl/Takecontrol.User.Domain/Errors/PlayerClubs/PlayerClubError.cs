using Takecontrol.Shared.Domain.Primitives;
using Takecontrol.User.Domain.Errors.Players;

namespace Takecontrol.User.Domain.Errors.PlayerClubs;

public sealed class PlayerClubError : DomainError
{
    public PlayerClubError(int codeId, string message) : base(codeId, message)
    {
    }

    public static PlayerClubError PlayerDoesntBelongToTheClub = new PlayerClubError(2001, "Player does not belong to the club");

}
