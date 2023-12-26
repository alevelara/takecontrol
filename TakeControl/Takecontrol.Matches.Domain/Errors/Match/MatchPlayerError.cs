using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Errors.Match;

public sealed class MatchPlayerError : DomainError
{
    public MatchPlayerError(int codeId, string message) : base(codeId, message)
    {
    }

    public static MatchPlayerError PlayerAlreadyRegistered = new MatchPlayerError(1301, "Player is already registered on this match");
    public static MatchPlayerError MatchPlayerNotFound = new MatchPlayerError(1302, "This player is not joing to any match");

}
