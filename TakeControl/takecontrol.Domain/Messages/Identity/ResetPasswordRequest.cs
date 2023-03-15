namespace Takecontrol.Domain.Messages.Identity;

public sealed class ResetPasswordRequest
{
    public string Email { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
}
