namespace Takecontrol.Credential.Domain.Messages.Identity;

public sealed record class AuthRequest(string Email, string Password);
