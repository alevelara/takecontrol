using System.ComponentModel.DataAnnotations;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Addresses.ValueObjects;
using takecontrol.Domain.Models.Clubs.ValueObjects;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Clubs;

public class Club : BaseDomainModel
{
    [Key]
    public ClubId Id { get;}
    public AddresId AddresId { get;}
    public string Name { get;} = string.Empty;
    public string Code { get; }

    [Required]
    public virtual Address Address { get; set; }

    private Club(AddresId addresId, string name, string code)
    {
        Id = new ClubId();
        AddresId = addresId;
        Name = name;
        Code = code;
    }

    public static Club Create(AddresId addresId, string name, string code)
    {
        return new Club(addresId, name, code);
    }
}

