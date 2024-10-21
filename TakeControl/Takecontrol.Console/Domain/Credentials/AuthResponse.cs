namespace Takecontrol.Console.Domain.Credentials;

public record class AuthResponse(Guid Id, string UserName, string Email, string Token, int UserType);
