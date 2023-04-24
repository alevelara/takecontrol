using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Matches.ValueObjects;

public class MatchId : ValueObject
{
    public Guid Value { get; private set; }

    public MatchId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
