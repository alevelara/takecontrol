using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Domain.Models.Players.ValueObjects;

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
