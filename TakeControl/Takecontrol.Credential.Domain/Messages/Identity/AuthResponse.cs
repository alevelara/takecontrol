using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;

namespace Takecontrol.Credential.Domain.Messages.Identity;

public sealed record class AuthResponse (Guid Id, string UserName, string Email, string Token, UserType UserType);