using takecontrol.Identity.Models.Enum;

namespace takecontrol.Domain.Mappings.Identity;

public class AuthResponse
{
    public string Id { get; set; } = String.Empty;
    public string UserName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
    public UserType UserType { get; set; }  
}
