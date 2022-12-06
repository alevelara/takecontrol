﻿using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Clubs.ValueObjects;

public class ClubId : ValueObject
{
    public Guid Value { get; }

    public ClubId()
    {
        Value = Guid.NewGuid();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
