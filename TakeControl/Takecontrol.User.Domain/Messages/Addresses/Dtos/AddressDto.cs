namespace Takecontrol.User.Domain.Messages.Addresses.Dtos;

public sealed record class AddressDto(
    Guid Id,
    string City,
    string Province,
    string MainAddress
    );
