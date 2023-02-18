using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs.ValueObjects;
using takecontrol.Domain.Models.PlayerClubs;
using takecontrol.Domain.Primitives;

namespace takecontrol.Domain.Models.Clubs;

public class Club : BaseDomainModel
{
    [Key]
    public Guid Id { get; private set; }

    [Required]
    public Guid AddresId { get; private set; }

    [Required]
    public Guid UserId { get; private set; }

    [Required]
    public string Name { get; private set; }

    [Required]
    public string Code { get; private set; }

    public virtual Address Address { get; set; }
    public virtual ICollection<PlayerClub> PlayerClubs { get; set; }

    private Club(Guid id, Guid addresId, Guid userId, string name, string code)
    {
        Id = id;
        AddresId = addresId;
        UserId = userId;
        Name = name;
        Code = code;
    }

    public static Club Create(Guid addresId, Guid userId, string name)
    {
        ClubValueObject clubValueObject = new();
        return new Club(clubValueObject.Value, addresId, userId, name, clubValueObject.Code);
    }
}