using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Errors.Match;

public sealed class MatchError : DomainError
{
    public MatchError(int codeId, string message) : base(codeId, message)
    {
    }

    public static MatchError MatchCreatedInAReservationCompleted = new MatchError(1801, "Match can not be created in this completed reservartion");
    public static MatchError MatchNotFound = new MatchError(1802, "Match does not exist.");
    public static MatchError MatchCompleted = new MatchError(1803, "Match is already completed");
    public static MatchError MatchCanNotBeCancelledByThisPlayer = new MatchError(1804, "Match can not be cancelled by this player");
    public static MatchError MatchCancelled = new MatchError(1805, "Match was previously cancelled");
    public static MatchError MatchCanNotBeCancelledByThisClub = new MatchError(1806, "Match can not be cancelled by this club");
}
