﻿using Takecontrol.Domain.Primitives;

namespace Takecontrol.Domain.Models.Clubs.ValueObjects;

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
