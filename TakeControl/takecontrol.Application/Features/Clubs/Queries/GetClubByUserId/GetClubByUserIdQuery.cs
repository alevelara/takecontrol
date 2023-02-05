using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Queries.GetByUserId
{
    public sealed record class GetClubByUserIdQuery(Guid UserId)
        : IQuery<Club>
    {
    }
}
