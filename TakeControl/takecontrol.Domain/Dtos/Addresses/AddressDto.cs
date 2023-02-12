namespace takecontrol.Domain.Dtos.Addresses;

public sealed class AddressDto
{
    public Guid Id { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string MainAddress { get; set; }
}
