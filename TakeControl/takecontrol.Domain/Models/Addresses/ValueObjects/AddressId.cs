using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Addresses.ValueObjects;

public class AddresId : ValueObject
{
    public Guid Value { get; }

    public AddresId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
