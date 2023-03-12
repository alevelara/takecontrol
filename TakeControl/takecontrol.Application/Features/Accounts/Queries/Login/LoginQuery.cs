using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Domain.Messages.Identity;

namespace takecontrol.Application.Features.Accounts.Queries.Login;

public class LoginQuery : IQuery<AuthResponse>
{
    public LoginQuery(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}
