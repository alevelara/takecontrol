using takecontrol.Application.Abstractions.Mediatr;
using takecontrol.Application.Contracts.Persitence.Players;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Players.Queries.GetPlayerById;
using takecontrol.Domain.Errors.Players;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Features.Players.Queries.GetPlayerById;

public sealed class GetPlayerByIdQueryHandler : IQueryHandler<GetPlayerByIdQuery, Player>
{
    private readonly IPlayerReadRepository _playerReadRepository;

    public GetPlayerByIdQueryHandler(IPlayerReadRepository playerReadRepository)
    {
        _playerReadRepository = playerReadRepository;
    }

    public async Task<Player> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerReadRepository.GetPlayerById(request.Id);
        if (player == null)
            throw new NotFoundException(PlayerError.PlayerNotFound);

        return player;
    }
}