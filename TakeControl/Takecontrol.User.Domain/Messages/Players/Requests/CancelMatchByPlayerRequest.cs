namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class CancelMatchByPlayerRequest(Guid PlayerId, Guid MatchId);