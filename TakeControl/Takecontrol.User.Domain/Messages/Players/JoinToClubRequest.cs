namespace Takecontrol.User.Domain.Messages.Players;

public sealed record class JoinToClubRequest(Guid PlayerId, Guid ClubId, string Code);
