using takecontrol.Domain.Dtos.Addresses;
using takecontrol.Domain.Models.Players.Enums;

namespace takecontrol.Domain.Dtos.Players;

public sealed class PlayerDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public int NumberOfClassesInAWeek { get; set; }

    public int AvgNumberOfMatchesInAWeek { get; set; }

    public int NumberOfYearsPlayed { get; set; }

    public int PlayerLevel { get; set; }
}
