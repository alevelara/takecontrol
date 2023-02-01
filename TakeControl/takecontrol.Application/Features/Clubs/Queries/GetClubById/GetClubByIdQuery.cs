using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Queries.GetById
{
    public sealed record class GetClubByIdQuery(Guid Id)
        : IQuery<Club>
    {
    }
}
