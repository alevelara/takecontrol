using Takecontrol.Domain.Models.Addresses.ValueObjects;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Domain.Primitives;

namespace Takecontrol.Domain.Models.Addresses;

public class Address : BaseDomainModel
{
    public Guid Id { get; private set; }
    public string City { get; private set; }
    public string Province { get; private set; }
    public string MainAddress { get; private set; }

    public virtual Club Club { get; set; }

    private Address(string city, string province, string mainAddress)
    {
        Id = new AddresId().Value;
        City = city;
        Province = province;
        MainAddress = mainAddress;
    }

    public static Address Create(string city, string province, string mainAddress)
    {
        return new Address(city, province, mainAddress);
    }
}
