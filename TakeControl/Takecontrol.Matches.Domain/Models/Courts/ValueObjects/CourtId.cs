using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Courts.ValueObjects;

public class CourtId : ValueObject
{
    public Guid Value { get; private set; }

    public CourtId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
