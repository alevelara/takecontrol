using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchesInfo;
using Takecontrol.Matches.Domain.Models.Matches.Documents;
using Takecontrol.Matches.Domain.Models.Matches.Options;

namespace Takecontrol.Matches.Infrastructure.Repositories.MatchesInfo;

public class MatchInfoReadRepository : IMatchInfoReadRepository
{
    private readonly IMongoCollection<MatchInfo> _matchInfoCollection;

    public MatchInfoReadRepository(IOptions<MatchDatabaseSettings> settings)
    {
        var mongoClient = new MongoClient(settings.Value.ConnectionString);
        var db = mongoClient.GetDatabase(settings.Value.DatabaseName);
        _matchInfoCollection = db.GetCollection<MatchInfo>(settings.Value.MatchCollectionName);
    }

    public async Task<List<MatchInfo>> GetAllAsync()
    {
        return await _matchInfoCollection.Find(_ => true).ToListAsync();
    }

    public async Task<List<MatchInfo>> GetAsync(Expression<Func<MatchInfo, bool>> predicate)
    {
        return await _matchInfoCollection.Find(predicate).ToListAsync();
    }

    public async Task<MatchInfo> GetMatchInfoByIdAsync(Guid matchId)
    {
        return await _matchInfoCollection.Find(x => x.MatchId == matchId).SingleAsync();
    }
}
