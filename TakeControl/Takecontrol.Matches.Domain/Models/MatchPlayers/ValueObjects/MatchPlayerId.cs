using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.MatchPlayers.ValueObjects;

public class MatchPlayerId : ValueObject
{
    public Guid Value { get; private set; }

    public MatchPlayerId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
