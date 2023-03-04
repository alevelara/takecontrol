namespace takecontrol.Domain.Dtos.Addresses;

public sealed class AddressDto
{
    public Guid Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Province { get; set; } = string.Empty;
    public string MainAddress { get; set; } = string.Empty;
}
