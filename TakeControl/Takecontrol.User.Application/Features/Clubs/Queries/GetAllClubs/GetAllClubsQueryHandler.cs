using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Domain.Models.Clubs;

namespace Takecontrol.User.Application.Features.Clubs.Queries.GetAllClubs;

public class GetAllClubsQueryHandler : IQueryHandler<GetAllClubsQuery, List<Club>>
{
    private readonly IClubReadRepository _clubReadRepository;

    public GetAllClubsQueryHandler(IClubReadRepository clubReadRepository)
    {
        _clubReadRepository = clubReadRepository;
    }

    public Task<List<Club>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
    {
        return _clubReadRepository.GetAllClubsAsync();
    }
}
