using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.User.Application.Features.Clubs.Queries.GetClubByUserId;

public sealed record class GetClubByUserIdQuery(Guid UserId)
    : IQuery<Club>
{
}
