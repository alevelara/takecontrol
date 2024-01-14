using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Queries.GetAllPlayersByClubId;

public sealed record class GetAllPlayersByClubIdQuery(Guid ClubId)
    : IQuery<List<Player>>
{
}
