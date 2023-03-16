using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Features.Players.Queries.GetPlayerByUserId
{
    public sealed record class GetPlayerByIdQuery(Guid Id)
        : IQuery<Player>
    {
    }
}
