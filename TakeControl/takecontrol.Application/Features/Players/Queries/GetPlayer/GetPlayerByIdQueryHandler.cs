using Takecontrol.Application.Abstractions.Mediatr;
using Takecontrol.Application.Contracts.Persitence.Players;
using Takecontrol.Application.Exceptions;
using Takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;
using Takecontrol.Domain.Errors.Players;
using Takecontrol.Domain.Models.Players;

namespace Takecontrol.Application.Features.Players.Queries.GetPlayerByUserId;

public sealed class GetPlayerByIdQueryHandler : IQueryHandler<GetPlayerByIdQuery, Player>
{
    private readonly IPlayerReadRepository _playerReadRepository;

    public GetPlayerByIdQueryHandler(IPlayerReadRepository playerReadRepository)
    {
        _playerReadRepository = playerReadRepository;
    }

    public async Task<Player> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerReadRepository.GetPlayerByUserId(request.Id);
        if (player == null)
            throw new NotFoundException(PlayerError.PlayerNotFound);

        return player;
    }
}
