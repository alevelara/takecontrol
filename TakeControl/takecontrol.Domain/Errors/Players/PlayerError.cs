namespace Takecontrol.Domain.Errors.Players;
using Takecontrol.Domain.Primitives;

public sealed class PlayerError : DomainError
{
    public PlayerError(int codeId, string message) : base(codeId, message)
    {
    }

    public static PlayerError PlayerNotFound = new PlayerError(1801, "Player doesn't exists for this id.");
}
