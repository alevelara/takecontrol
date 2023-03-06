using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence.Clubs;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Clubs.Queries.GetByUserId;
using takecontrol.Domain.Errors.Clubs;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Queries.GetClubById;

public sealed class GetClubByUserIdQueryHandler : IQueryHandler<GetClubByUserIdQuery, Club>
{
    private readonly IClubReadRepository _clubReadRepository;

    public GetClubByUserIdQueryHandler(IClubReadRepository clubReadRepository)
    {
        _clubReadRepository = clubReadRepository;
    }

    public async Task<Club> Handle(GetClubByUserIdQuery request, CancellationToken cancellationToken)
    {
        var club = await _clubReadRepository.GetClubByUserId(request.UserId);
        if (club == null)
            throw new NotFoundException(ClubError.ClubNotFound);

        return club;
    }
}
