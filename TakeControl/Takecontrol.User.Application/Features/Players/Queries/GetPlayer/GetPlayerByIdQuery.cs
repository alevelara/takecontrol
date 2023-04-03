using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Queries.GetPlayer
{
    public sealed record class GetPlayerByIdQuery(Guid Id)
        : IQuery<Player>
    {
    }
}
