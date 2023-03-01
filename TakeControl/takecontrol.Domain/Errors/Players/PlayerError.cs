using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Errors.Clubs;

public sealed class PlayerError : DomainError
{
    public PlayerError(int codeId, string message) : base(codeId, message)
    {
    }

    
}
