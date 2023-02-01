using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Application.Features.Clubs.Queries.GetById;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Features.Clubs.Queries.GetClubById;

public sealed class GetClubByIdQueryHandler : IQueryHandler<GetClubByIdQuery, Club>
{
    private readonly IClubReadRepository _clubReadRepository;

    public GetClubByIdQueryHandler(IClubReadRepository clubReadRepository)
    {
        _clubReadRepository = clubReadRepository;
    }

    public async Task<Club> Handle(GetClubByIdQuery request, CancellationToken cancellationToken)
    {
        return await _clubReadRepository.GetClubById(request.Id);
    }
}
