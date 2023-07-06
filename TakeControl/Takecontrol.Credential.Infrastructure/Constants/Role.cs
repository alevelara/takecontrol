namespace Takecontrol.Credential.Infrastructure.Constants;

public static class Role
{
    public const string Administrator = "Administrator";
    public const string Club = "Club";
    public const string Player = "Player";
    public const string Users = $"{Role.Club},{Role.Player}";
}
