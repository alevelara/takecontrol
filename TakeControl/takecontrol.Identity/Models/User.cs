using Microsoft.AspNetCore.Identity;

namespace takecontrol.Identity.Models;

public class User : IdentityUser
{
    public string Name { get; set; }
    public string SurName { get; set; }
}
