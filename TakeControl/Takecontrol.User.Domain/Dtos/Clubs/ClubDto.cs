using Takecontrol.User.Domain.Dtos.Addresses;

namespace Takecontrol.User.Domain.Dtos.Clubs;

public sealed record class ClubDto(
    Guid Id,
    Guid UserId,
    string Name,
    string Code,
    AddressDto Address
    );