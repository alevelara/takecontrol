using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Domain.Models.Addresses.ValueObjects;

public class AddresId : ValueObject
{
    public Guid Value { get; private set; }

    public AddresId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}