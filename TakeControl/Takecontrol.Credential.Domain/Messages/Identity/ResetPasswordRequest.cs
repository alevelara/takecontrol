namespace Takecontrol.Credential.Domain.Messages.Identity;

public sealed record class ResetPasswordRequest(string Email, string CurrentPassword, string NewPassword);
