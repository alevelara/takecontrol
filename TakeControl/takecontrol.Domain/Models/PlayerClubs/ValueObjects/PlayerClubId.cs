﻿using Takecontrol.Domain.Primitives;

namespace Takecontrol.Domain.Models.PlayerClubs.ValueObjects;

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
