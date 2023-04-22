using Takecontrol.User.Domain.Messages.Addresses.Dtos;

namespace Takecontrol.User.Domain.Messages.Clubs.Dtos;

public sealed record class ClubDto(
    Guid Id,
    Guid UserId,
    string Name,
    string Code,
    AddressDto Address
    );