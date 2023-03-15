using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Persitence.Clubs;
using Takecontrol.Domain.Models.Clubs;

namespace Takecontrol.Application.Features.Clubs.Queries.GetAllClubs;

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
