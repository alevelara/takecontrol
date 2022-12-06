using Microsoft.AspNetCore.Identity;
using takecontrol.Identity.Models.Enum;

namespace takecontrol.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = String.Empty;
    public UserType UserType{ get; set; }
}
