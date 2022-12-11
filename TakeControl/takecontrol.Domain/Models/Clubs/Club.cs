using System.ComponentModel.DataAnnotations;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs.ValueObjects;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Clubs;

public class Club : BaseDomainModel
{
    [Key]
    public Guid Id { get; private set; }
    public Guid AddresId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; }
    
    public virtual Address Address { get; set; }

    private Club(Guid addresId, string name, string code)
    {
        Id = new ClubId().Value;
        AddresId = addresId;
        Name = name;
        Code = code;
    }

    public static Club Create(Guid addresId, string name, string code)
    {
        return new Club(addresId, name, code);
    }
}

