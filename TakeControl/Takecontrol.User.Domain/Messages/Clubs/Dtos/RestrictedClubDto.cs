using Takecontrol.User.Domain.Messages.Addresses.Dtos;

namespace Takecontrol.User.Domain.Messages.Clubs.Dtos;

public sealed record class RestrictedClubDto(
    Guid Id,
    Guid UserId,
    string Name,
    int NumberOfCourts,
    AddressDto Address
    );
