using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Domain.Errors.Players;

namespace Takecontrol.User.Application.Features.Players.Commands.CancelMatch;

public sealed class CancelMatchCommandHandler : ICommandHandler<CancelMatchCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IPlayerReadRepository _playerReadRepository;

    public CancelMatchCommandHandler(IMediator mediator, IPlayerReadRepository playerReadRepository)
    {
        _mediator = mediator;
        _playerReadRepository = playerReadRepository;
    }

    public async Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerReadRepository.GetPlayerByUserId(request.UserId);

        if (player == null)
            throw new NotFoundException(PlayerError.PlayerNotFound);

        var command = new CancelMatchByPlayerCommand(player.Id, request.MatchId);

        return await _mediator.Send(command);
    }
}
