using Takecontrol.Domain.Primitives;
using Takecontrol.Domain.Utils;

namespace Takecontrol.Domain.Models.Clubs.ValueObjects;

public class ClubValueObject : ValueObject
{
    public Guid Value { get; private set; }

    public string Code { get; private set; }

    public ClubValueObject()
    {
        Value = Guid.NewGuid();
        Code = RandomGenerator.RandomString(5);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Code;
    }
}
