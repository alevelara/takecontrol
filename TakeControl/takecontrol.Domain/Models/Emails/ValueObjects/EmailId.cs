using Takecontrol.Domain.Primitives;

namespace Takecontrol.Domain.Models.Emails.ValueObjects;

public class EmailId : ValueObject
{
    public Guid Value { get; private set; }

    public EmailId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
