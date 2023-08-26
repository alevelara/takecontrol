using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Domain.Errors.Players;
public sealed class PlayerError : DomainError
{
    public PlayerError(int codeId, string message) : base(codeId, message)
    {
    }

    public static PlayerError PlayerNotFound = new PlayerError(1701, "Player doesn't exists for this id.");
}
