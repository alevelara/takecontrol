using Microsoft.AspNetCore.Identity;
using Takecontrol.Domain.Models.ApplicationUser.Enum;

namespace Takecontrol.Identity.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public UserType UserType { get; set; }
}
