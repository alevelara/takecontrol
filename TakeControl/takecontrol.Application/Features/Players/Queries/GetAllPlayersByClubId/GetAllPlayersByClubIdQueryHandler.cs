using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence.PlayerClubs;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;

public sealed class GetAllPlayersByClubIdQueryHandler : IQueryHandler<GetAllPlayersByClubIdQuery, List<Player>>
{
    private readonly IPlayerClubsReadRepository _playerClubsReadRepository;

    public GetAllPlayersByClubIdQueryHandler(IPlayerClubsReadRepository playerClubsReadRepository)
    {
        _playerClubsReadRepository = playerClubsReadRepository;
    }

    public async Task<List<Player>> Handle(GetAllPlayersByClubIdQuery request, CancellationToken cancellationToken)
    {
        var playersByClub = await _playerClubsReadRepository.GetAllPlayersByClubId(request.Id);
        
        return playersByClub!.Select(c => c.Player).ToList();
    }
}
