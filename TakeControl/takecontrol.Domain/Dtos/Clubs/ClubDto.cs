using takecontrol.Domain.Models.Addresses;

namespace takecontrol.Domain.Dtos.Clubs;

public sealed class ClubDto
{
    public Guid Id { get; set; }

    public Guid AddresId { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public Address Address { get; set; }
}
