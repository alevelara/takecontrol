using Takecontrol.Domain.Models.ApplicationUser.Enum;

namespace Takecontrol.Domain.Messages.Identity;

public class AuthResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public UserType UserType { get; set; }
}
