using System.ComponentModel.DataAnnotations;
using Takecontrol.Shared.Domain.Primitives;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs.ValueObjects;
using Takecontrol.User.Domain.Models.PlayerClubs;

namespace Takecontrol.User.Domain.Models.Clubs;

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

    [Required]
    public int NumberOfCourts { get; private set; }

    public virtual Address Address { get; set; }
    public virtual ICollection<PlayerClub> PlayerClubs { get; set; }

    private Club(Guid id, Guid addresId, Guid userId, string name, string code, int numberOfCourts)
    {
        Id = id;
        AddresId = addresId;
        UserId = userId;
        Name = name;
        Code = code;
        NumberOfCourts = numberOfCourts;
    }

    public static Club Create(Guid addresId, Guid userId, string name, int numberOfCourts)
    {
        ClubValueObject clubValueObject = new();
        return new Club(clubValueObject.Value, addresId, userId, name, clubValueObject.Code, numberOfCourts);
    }
}