using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Domain.Errors.Clubs;

public sealed class ClubError : DomainError
{
    public ClubError(int codeId, string message) : base(codeId, message)
    {
    }

    public static ClubError ClubNotFound = new ClubError(1601, "Club doesn't exists for this userId.");
    public static ClubError ClubDoesnotMatchByCode = new ClubError(1602, "Club is not found by code.");
}
