namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class CancelMatchRequest(Guid PlayerId, Guid MatchId);