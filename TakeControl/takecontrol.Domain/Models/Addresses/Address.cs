using System.ComponentModel.DataAnnotations;
using takecontrol.Domain.Models.Addresses.ValueObjects;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Addresses;

public class Address : BaseDomainModel
{
    public Guid Id { get; private set; }
    public string City { get; private set; } = string.Empty;
    public string Province { get; private set; } = string.Empty;
    public string MainAddress { get; private set; } = string.Empty;

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
