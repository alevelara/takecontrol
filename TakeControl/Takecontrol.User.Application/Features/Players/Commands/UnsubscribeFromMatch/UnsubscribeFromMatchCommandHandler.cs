using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Domain.Errors.Players;

namespace Takecontrol.User.Application.Features.Players.Commands.UnsubscribeFromMatch;

public sealed class UnsubscribeFromMatchCommandHandler : ICommandHandler<UnsubscribeFromMatchCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IPlayerReadRepository _playerRepository;

    public UnsubscribeFromMatchCommandHandler(IMediator mediator, IPlayerReadRepository playerRepository)
    {
        _mediator = mediator;
        _playerRepository = playerRepository;
    }

    public async Task<Unit> Handle(UnsubscribeFromMatchCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetPlayerByUserId(request.UserId);
        if (player == null)
        {
            throw new NotFoundException(PlayerError.PlayerNotFound);
        }

        var command = new UnsubscribePlayerFromMatchCommand(player.Id, request.MatchId);
        return await _mediator.Send(command);
    }
}
