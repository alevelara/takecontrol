using Takecontrol.Matches.Domain.Models.Courts.ValueObjects;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Domain.Models.Courts;

public class Court : BaseDomainModel
{
    public Guid Id { get; private set; }
    public Guid ClubId { get; private set; }
    public string Name { get; private set; }

    public virtual ICollection<Reservation> Reservations { get; set; }

    private Court(Guid clubId, string name)
    {
        Id = new CourtId().Value;
        ClubId = clubId;
        Name = name;
    }

    public static Court Create(Guid clubId, string name)
    {
        return new Court(clubId, name);
    }
}
