namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class UnregisterFromClubRequest(Guid UserId, Guid ClubId);
