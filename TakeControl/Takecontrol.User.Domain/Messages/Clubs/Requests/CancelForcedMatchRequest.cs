namespace Takecontrol.User.Domain.Messages.Clubs.Requests;

public sealed record class CancelForcedMatchRequest(Guid clubId, Guid matchId, string description);
