using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Domain.Errors.Players;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToAMatch;

public sealed class JoinToAMatchCommandHandler : ICommandHandler<JoinToAMatchCommand, Unit>
{
    private readonly IPlayerReadRepository _playerReadRepository;
    private readonly IMediator _mediator;

    public JoinToAMatchCommandHandler(IPlayerReadRepository playerReadRepository, IMediator mediator)
    {
        _playerReadRepository = playerReadRepository;
        _mediator = mediator;
    }

    public async Task<Unit> Handle(JoinToAMatchCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerReadRepository.GetPlayerByUserId(request.UserId);
        if (player == null)
        {
            throw new NotFoundException(PlayerError.PlayerNotFound);
        }

        var command = new JoinToMatchCommand(player.Id, request.MatchId);

        return await _mediator.Send(command);
    }
}
