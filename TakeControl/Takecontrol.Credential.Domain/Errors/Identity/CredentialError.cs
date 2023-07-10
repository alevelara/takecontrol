using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Credential.Domain.Errors.Identity;

public sealed class CredentialError : DomainError
{
    public CredentialError(int codeId, string message) : base(codeId, message)
    {
    }

    public static CredentialError UserDoesntExist = new CredentialError(1401, "User doesn't exists.");
    public static CredentialError InvalidEmailForUser = new CredentialError(1402, "User email was not properly register.");
    public static CredentialError InvalidUserNameForUser = new CredentialError(1403, "User name was not properly register.");
    public static CredentialError InvalidSecurtyStampNameForUser = new CredentialError(1404, "Security stamp was not properly register.");
    public static CredentialError InvalidCredentials = new CredentialError(1405, "Invalid credentials.");
    public static CredentialError UserAlreadyExistsWithThisEmail = new CredentialError(1406, "This email is already used.");
    public static CredentialError UserAlreadyExistsWithThisUserName = new CredentialError(1407, "This username is already used.");
    public static CredentialError ErrorDuringUserRegistration = new CredentialError(1408, "Something happened during user registration.");
    public static CredentialError ErrorChangingPassword = new CredentialError(1409, "Something happened while changing password.");
    public static CredentialError ErrorGeneratingUpdatePassword = new CredentialError(1410, "Something happened in generating a password update.");
}
