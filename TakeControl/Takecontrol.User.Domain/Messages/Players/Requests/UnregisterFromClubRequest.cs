namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class UnregisterFromClubRequest(Guid UserPlayerId, Guid UserClubId);
