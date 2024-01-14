namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class JoinToAMatchRequest(Guid UserId, Guid MatchId);
