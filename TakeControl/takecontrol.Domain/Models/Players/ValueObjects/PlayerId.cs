using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Clubs.ValueObjects;

public class PlayerId : ValueObject
{
    public Guid Value { get; private set; }

    public PlayerId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
