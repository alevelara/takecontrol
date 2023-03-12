namespace Takecontrol.User.Domain.Dtos.Addresses;

public sealed record class AddressDto(
    Guid Id,
    string City,
    string Province,
    string MainAddress
    );
