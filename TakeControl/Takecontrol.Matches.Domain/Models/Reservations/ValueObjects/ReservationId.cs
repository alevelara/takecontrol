using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Reservations.ValueObjects;

public class ReservationId : ValueObject
{
    public Guid Value { get; private set; }

    public ReservationId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
