using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence.Clubs;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Queries.GetAllClubs;

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
