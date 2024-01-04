namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class UnregisterFromClubRequest(Guid userId, Guid clubId);
