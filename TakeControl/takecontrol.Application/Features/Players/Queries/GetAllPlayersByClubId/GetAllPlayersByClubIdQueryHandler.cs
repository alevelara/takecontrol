using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Persitence.PlayerClubs;
using Takecontrol.Domain.Models.Players;

namespace Takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;

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
