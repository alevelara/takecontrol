﻿using takecontrol.Domain.Dtos.Addresses;

namespace takecontrol.Domain.Dtos.Clubs;

public sealed class ClubDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public AddressDto Address { get; set; }
}
