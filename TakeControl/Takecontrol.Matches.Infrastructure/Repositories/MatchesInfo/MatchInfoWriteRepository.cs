using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchesInfo;
using Takecontrol.Matches.Domain.Models.Matches.Documents;
using Takecontrol.Matches.Domain.Models.Matches.Options;

namespace Takecontrol.Matches.Infrastructure.Repositories.MatchesInfo;

public class MatchInfoWriteRepository : IMatchInfoWriteRepository
{
    private readonly IMongoCollection<MatchInfo> _matchInfoCollection;

    public MatchInfoWriteRepository(IOptions<MatchDatabaseSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var db = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _matchInfoCollection = db.GetCollection<MatchInfo>(settings.Value.MatchCollectionName);
    }

    public async Task CreateAsync(MatchInfo matchInfo, CancellationToken cancellationToken)
    {
        await _matchInfoCollection.InsertOneAsync(matchInfo, null, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _matchInfoCollection.DeleteOneAsync(x => x.MatchId == id, null, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, MatchInfo matchInfo, CancellationToken cancellationToken)
    {
        await _matchInfoCollection.ReplaceOneAsync(x => x.MatchId == id, matchInfo, new ReplaceOptions() { }, cancellationToken);
    }
}
