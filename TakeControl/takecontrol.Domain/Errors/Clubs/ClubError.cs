using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Errors.Clubs;

public sealed class ClubError : DomainError
{
    public ClubError(int codeId, string message) : base(codeId, message)
    {
    }

    public static ClubError ClubNotFound = new ClubError(1701, "Club doesn't exists for this userId.");
}
