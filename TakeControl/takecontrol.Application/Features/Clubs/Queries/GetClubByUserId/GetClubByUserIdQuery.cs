using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Domain.Models.Clubs;

namespace Takecontrol.Application.Features.Clubs.Queries.GetByUserId
{
    public sealed record class GetClubByUserIdQuery(Guid UserId)
        : IQuery<Club>
    {
    }
}
