using Takecontrol.User.Domain.Dtos.Addresses;

namespace Takecontrol.User.Domain.Dtos.Clubs;

public sealed record class RestrictedClubDto(
    Guid Id,
    Guid UserId,
    string Name,
    AddressDto Address
    );
