using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Credential.Application.Features.Accounts.Queries.Login;

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
