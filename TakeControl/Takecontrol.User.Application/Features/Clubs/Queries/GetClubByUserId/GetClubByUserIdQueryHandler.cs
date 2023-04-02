using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Domain.Errors.Clubs;
using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.User.Application.Features.Clubs.Queries.GetClubByUserId;

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
