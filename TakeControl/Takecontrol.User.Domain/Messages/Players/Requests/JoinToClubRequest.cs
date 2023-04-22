namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class JoinToClubRequest(Guid PlayerId, Guid ClubId, string Code);
