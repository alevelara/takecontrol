using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Errors.Match;

public sealed class CourtError : DomainError
{
    public CourtError(int codeId, string message) : base(codeId, message)
    {
    }

    public static CourtError CourtNotFound = new CourtError(1701, "Court not found.");

}
