using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Matches.Documents;

public class MatchInfo : BaseDomainModel
{
    public Guid MatchId { get; private set; }
    public ICollection<string> PlayerNames { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string CourtName { get; private set; }
    public string ClubName { get; private set; }

    private MatchInfo(Guid matchId, ICollection<string> playerNames, DateTime startDate, DateTime endDate, string courtName, string clubName)
    {
        MatchId = matchId;
        PlayerNames = playerNames;
        StartDate = startDate;
        EndDate = endDate;
        CourtName = courtName;
        ClubName = clubName;
    }

    public static MatchInfo Create(Guid matchId, ICollection<string> playerNames, DateTime startDate, DateTime endDate, string courtName, string clubName)
    {
        return new MatchInfo(matchId, playerNames, startDate, endDate, courtName, clubName);
    }
}
