using Microsoft.AspNetCore.Identity;
using Takecontrol.Credential.Domain.Models.ApplicationUser.Enum;

namespace Takecontrol.Credential.Infrastructure.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public UserType UserType { get; set; }
}
