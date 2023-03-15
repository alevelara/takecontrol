using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Domain.Models.Players;

namespace Takecontrol.Application.Features.Players.Queries.GetPlayerByUserId
{
    public sealed record class GetPlayerByIdQuery(Guid Id)
        : IQuery<Player>
    {
    }
}
