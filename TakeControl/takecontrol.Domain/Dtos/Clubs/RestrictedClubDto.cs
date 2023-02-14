using takecontrol.Domain.Dtos.Addresses;

namespace takecontrol.Domain.Dtos.Clubs;

public sealed class RestrictedClubDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public AddressDto Address { get; set; }
}
