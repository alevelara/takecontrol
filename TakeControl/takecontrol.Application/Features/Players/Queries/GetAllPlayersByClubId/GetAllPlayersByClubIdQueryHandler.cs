using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence.Players;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;

public sealed class GetAllPlayersByClubIdQueryHandler : IQueryHandler<GetAllPlayersByClubIdQuery, List<Player>>
{
    private readonly IPlayerReadRepository _playerReadRepository;

    public GetAllPlayersByClubIdQueryHandler(IPlayerReadRepository playerReadRepository)
    {
        _playerReadRepository = playerReadRepository;
    }

    public Task<List<Player>> Handle(GetAllPlayersByClubIdQuery request, CancellationToken cancellationToken)
    {
        return _playerReadRepository.GetAllPlayersByClubId(request.Id);
    }
}
