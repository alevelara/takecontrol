using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Domain.Models.PlayerClubs.ValueObjects;

public class PlayerClubId : ValueObject
{
    public Guid Value { get; private set; }

    public PlayerClubId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
