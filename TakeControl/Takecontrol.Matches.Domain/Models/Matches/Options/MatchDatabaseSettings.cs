namespace Takecontrol.Matches.Domain.Models.Matches.Options;

public class MatchDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string MatchCollectionName { get; set; } = null!;
}
