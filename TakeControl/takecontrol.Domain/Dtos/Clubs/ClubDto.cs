using takecontrol.Domain.Dtos.Addresses;

namespace takecontrol.Domain.Dtos.Clubs;

public sealed class ClubDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public AddressDto Address { get; set; } = default!;
}
