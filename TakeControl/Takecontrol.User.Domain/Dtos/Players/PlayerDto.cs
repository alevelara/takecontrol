namespace Takecontrol.User.Domain.Dtos.Players;

public sealed record class PlayerDto(
    Guid Id,
    Guid UserId,
    string Name,
    int NumberOfClassesInAWeek,
    int AvgNumberOfMatchesInAWeek,
    int NumberOfYearsPlayed,
    int PlayerLevel
    );
