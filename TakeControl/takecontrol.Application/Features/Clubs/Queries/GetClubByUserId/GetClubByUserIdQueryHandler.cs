using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Persitence.Clubs;
using Takecontrol.Application.Exceptions;
using Takecontrol.Application.Features.Clubs.Queries.GetByUserId;
using Takecontrol.Domain.Errors.Clubs;
using Takecontrol.Domain.Models.Clubs;

namespace Takecontrol.Application.Features.Clubs.Queries.GetClubById;

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
