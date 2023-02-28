using System.ComponentModel.DataAnnotations;
using takecontrol.Domain.Models.Clubs.ValueObjects;
using takecontrol.Domain.Models.PlayerClubs;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Players;

public class Player : BaseDomainModel
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    public Guid UserId { get; private set; }

    [Required]
    public string Name { get; private set; }

    public int NumberOfClassesInAWeek { get; private set; }

    public int AvgNumberOfMatchesInAWeek { get; private set; }

    public int NumberOfYearsPlayed { get; private set; }

    public int PlayerLevel { get; private set; }

    public virtual ICollection<PlayerClub> PlayerClubs { get; set; }

    private Player(Guid id, Guid userId, string name, int numberOfClassesInAWeek, int avgNumberOfMatchesInAWeek, int numberOfYearsPlayed)
    {
        Id = id;
        UserId = userId;
        Name = name;
        NumberOfClassesInAWeek = numberOfClassesInAWeek;
        AvgNumberOfMatchesInAWeek = avgNumberOfMatchesInAWeek;
        NumberOfYearsPlayed = numberOfYearsPlayed;
        PlayerLevel = CalculatePlayerLevel(numberOfClassesInAWeek, avgNumberOfMatchesInAWeek, numberOfYearsPlayed);
    }

    public static Player Create(Guid userId, string name, int numberOfClassesInAWeek, int avgNumberOfMatchesInAWeek, int numberOfYearsPlayed)
    {
        PlayerId playerId = new();
        return new Player(playerId.Value, userId, name, numberOfClassesInAWeek, avgNumberOfMatchesInAWeek, numberOfYearsPlayed);
    }

    //importante: 
    //numero de clases * 0.4
    //media de partidos * 0.2
    //numero de años jugando * 0.4
    //Si suma < 1.5 -> Begginer
    //Si suma > 1.5 y < 3 -> Mid
    //Si suma > 3 -> Expert
    private int CalculatePlayerLevel(int numberOfClassesInAWeek, int avgNumberOfMatchesInAWeek, int numberOfYearsPlayed)
    {
        const double minimumBegginerLevel = 0;
        const double minimunMidLevel = 1.5;
        const double minimumExpertLevel = 3;

        var levelPlayer = (numberOfClassesInAWeek * 0.4) + (avgNumberOfMatchesInAWeek * 0.2) + (numberOfYearsPlayed * 0.4);

        if (levelPlayer is > minimumBegginerLevel and < minimunMidLevel)
        {
            return (int) Players.Enums.PlayerLevel.Begginer;
        }

        if (levelPlayer is >= minimunMidLevel and < minimumExpertLevel)
        {
            return (int) Players.Enums.PlayerLevel.Mid;
        }

        if (levelPlayer is >= minimumExpertLevel)
        {
            return (int) Players.Enums.PlayerLevel.Expert;
        }

        return (int) Players.Enums.PlayerLevel.Begginer;
    }
}
