namespace Takecontrol.User.Domain.Messages.Players.Dtos;

public sealed record class PlayerDto(
    Guid Id,
    Guid UserId,
    string Name,
    int NumberOfClassesInAWeek,
    int AvgNumberOfMatchesInAWeek,
    int NumberOfYearsPlayed,
    int PlayerLevel
    );
