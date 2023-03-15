namespace Takecontrol.Domain.Messages.Players;

public class RegisterPlayerRequest
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int NumberOfClassesInAWeek { get; set; }

    public int AvgNumberOfMatchesInAWeek { get; set; }

    public int NumberOfYearsPlayed { get; set; }
}
