namespace Takecontrol.User.Domain.Messages.Clubs.Requests;

public sealed record class CancelForcedMatchRequest(Guid UserId, Guid MatchId, string Description);