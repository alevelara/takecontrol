namespace Takecontrol.User.Domain.Messages.Clubs.Requests;

public sealed record class CancelForcedMatchRequest(Guid ClubId, Guid MatchId, string Description);