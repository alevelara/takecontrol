namespace Takecontrol.Shared.Application.Dtos.Matches;

public sealed record class MatchDto(DateTime startDate, DateTime endDate, Guid clubId, string courtName, List<Guid> playerIds);