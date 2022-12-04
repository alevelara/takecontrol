using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Errors.Identity;

public sealed class IdentityError : DomainError
{
    public IdentityError(int codeId, string message) : base(codeId, message)
    {
    }

    public static IdentityError UserDoesntExist = new IdentityError(1501, "User doesn't exists.");
    public static IdentityError InvalidEmailForUser = new IdentityError(1502, "User email was not properly register.");
    public static IdentityError InvalidUserNameForUser = new IdentityError(1503, "User name was not properly register.");
    public static IdentityError InvalidSecurtyStampNameForUser = new IdentityError(1504, "Security stamp was not properly register.");
    public static IdentityError InvalidCredentials = new IdentityError(1505, "Invalid credentials");
}
