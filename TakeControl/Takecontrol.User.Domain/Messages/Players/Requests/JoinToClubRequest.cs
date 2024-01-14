namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class JoinToClubRequest(Guid UserPlayerId, Guid UserClubId, string Code);
