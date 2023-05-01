using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Emails.Domain.Models.Templates.ValueObjects;

public class TemplateId : ValueObject
{
    public Guid Value { get; private set; }

    public TemplateId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
