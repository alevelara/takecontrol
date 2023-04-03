using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;

namespace Takecontrol.Credential.Domain.Messages.Identity;

public sealed record class RegistrationRequest(string Name, string Email, string Password, UserType UserType);
