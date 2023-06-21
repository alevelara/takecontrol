using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Errors.Match;

public sealed class MatchError : DomainError
{
    public MatchError(int codeId, string message) : base(codeId, message)
    {
    }

    public static MatchError MatchCreatedInAReservationCompleted = new MatchError(1801, "Match can not be created in this completed reservartion");
}
