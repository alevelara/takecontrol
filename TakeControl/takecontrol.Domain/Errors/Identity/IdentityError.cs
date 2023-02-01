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
    public static IdentityError InvalidCredentials = new IdentityError(1505, "Invalid credentials.");
    public static IdentityError UserAlreadyExistsWithThisEmail = new IdentityError(1506, "This email is already used.");
    public static IdentityError UserAlreadyExistsWithThisUserName = new IdentityError(1507, "This username is already used.");
    public static IdentityError ErrorDuringUserRegistration = new IdentityError(1508, "Something happened during user registration.");
    public static IdentityError ErrorChangingPassword = new IdentityError(1509, "Something happened during password update.");
    public static IdentityError ErrorGeneratingUpdatePassword = new IdentityError(1509, "Something happened during password update.");
}
