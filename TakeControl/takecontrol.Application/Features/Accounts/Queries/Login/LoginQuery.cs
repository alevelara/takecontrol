using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Domain.Mappings.Identity;

namespace takecontrol.Application.Features.Accounts.Queries.Login;

public class LoginQuery : IQuery<AuthResponse>
{
    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}
