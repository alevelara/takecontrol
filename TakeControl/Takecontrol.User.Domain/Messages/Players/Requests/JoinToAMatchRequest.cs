namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class JoinToAMatchRequest(Guid PlayerId, Guid MatchId);
