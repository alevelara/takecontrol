using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Domain.Models.Players;

namespace Takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId
{
    public sealed record class GetAllPlayersByClubIdQuery(Guid Id)
        : IQuery<List<Player>>
    {
    }
}
