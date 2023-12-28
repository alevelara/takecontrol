using Takecontrol.API.Routes;

namespace Takecontrol.API.Tests.Helpers;

internal class Endpoints
{
    public const string Login = "api/v1/auth/Login";
    public const string ResetPasword = "api/v1/auth/ResetPassword";
    public const string UpdatePasword = "api/v1/auth/UpdatePassword";
    public const string RegisterPlayer = "api/v1/player/Register";
    public const string JoinToClub = "api/v1/player/Join";
    public const string JoinToMatch = "api/v1/player/JoinToMatch";
    public const string CancelMatch = "api/v1/player/CancelMatch";
    public const string UnsubscribeFromMatch = "api/v1/player/Unsubscribe";
    public const string CreateMatch = "api/v1/match/Create";
    public const string RegisterClub = "api/v1/club/Register";
    public const string AllClubs = "api/v1/club/all";
    public const string CancelForcedMatch = "api/v1/club/CancelForcedMatch";
}
