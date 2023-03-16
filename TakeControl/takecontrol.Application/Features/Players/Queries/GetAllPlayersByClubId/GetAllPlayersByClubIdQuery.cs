using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId
{
    public sealed record class GetAllPlayersByClubIdQuery(Guid Id)
        : IQuery<List<Player>>
    {
    }
}
