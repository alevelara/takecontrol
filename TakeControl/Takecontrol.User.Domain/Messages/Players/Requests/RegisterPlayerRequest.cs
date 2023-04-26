namespace Takecontrol.User.Domain.Messages.Players.Requests;

public sealed record class RegisterPlayerRequest(
    string Name,
    string Email,
    string Password,
    int NumberOfClassesInAWeek,
    int AvgNumberOfMatchesInAWeek,
    int NumberOfYearsPlayed);
