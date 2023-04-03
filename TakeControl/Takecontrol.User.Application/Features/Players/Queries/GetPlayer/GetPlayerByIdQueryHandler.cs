using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Domain.Errors.Players;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Queries.GetPlayer;

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
