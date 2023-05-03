using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Credential.Domain.Errors.Identity;

public sealed class CredentialError : DomainError
{
    public CredentialError(int codeId, string message) : base(codeId, message)
    {
    }

    public static CredentialError UserDoesntExist = new CredentialError(1501, "User doesn't exists.");
    public static CredentialError InvalidEmailForUser = new CredentialError(1502, "User email was not properly register.");
    public static CredentialError InvalidUserNameForUser = new CredentialError(1503, "User name was not properly register.");
    public static CredentialError InvalidSecurtyStampNameForUser = new CredentialError(1504, "Security stamp was not properly register.");
    public static CredentialError InvalidCredentials = new CredentialError(1505, "Invalid credentials.");
    public static CredentialError UserAlreadyExistsWithThisEmail = new CredentialError(1506, "This email is already used.");
    public static CredentialError UserAlreadyExistsWithThisUserName = new CredentialError(1507, "This username is already used.");
    public static CredentialError ErrorDuringUserRegistration = new CredentialError(1508, "Something happened during user registration.");
    public static CredentialError ErrorChangingPassword = new CredentialError(1509, "Something happened while changing password.");
    public static CredentialError ErrorGeneratingUpdatePassword = new CredentialError(1510, "Something happened in generating a password update.");
}
