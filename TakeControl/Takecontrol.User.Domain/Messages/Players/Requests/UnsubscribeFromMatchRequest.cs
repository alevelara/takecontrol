namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class UnsubscribeFromMatchRequest(Guid UserId, Guid MatchId);