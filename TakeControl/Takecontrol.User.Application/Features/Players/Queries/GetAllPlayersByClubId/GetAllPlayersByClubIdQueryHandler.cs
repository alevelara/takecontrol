using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Queries.GetAllPlayersByClubId;

public sealed class GetAllPlayersByClubIdQueryHandler : IQueryHandler<GetAllPlayersByClubIdQuery, List<Player>>
{
    private readonly IPlayerClubReadRepository _playerClubsReadRepository;

    public GetAllPlayersByClubIdQueryHandler(IPlayerClubReadRepository playerClubsReadRepository)
    {
        _playerClubsReadRepository = playerClubsReadRepository;
    }

    public async Task<List<Player>> Handle(GetAllPlayersByClubIdQuery request, CancellationToken cancellationToken)
    {
        var playersByClub = await _playerClubsReadRepository.GetAllPlayersByClubId(request.Id);

        return playersByClub!.Select(c => c.Player).ToList();
    }
}
