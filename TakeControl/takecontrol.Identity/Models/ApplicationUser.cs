using Microsoft.AspNetCore.Identity;
using takecontrol.Domain.Models.ApplicationUser.Enum;

namespace takecontrol.Identity.Models;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public UserType UserType { get; set; }   
}
