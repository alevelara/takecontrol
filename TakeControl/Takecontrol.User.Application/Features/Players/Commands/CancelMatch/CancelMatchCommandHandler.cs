using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Errors.Players;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Features.Players.Commands.CancelMatch;

public sealed class CancelMatchCommandHandler : ICommandHandler<CancelMatchCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IAsyncReadRepository<Player> _playerReadRepository;

    public CancelMatchCommandHandler(IMediator mediator, IAsyncReadRepository<Player> playerReadRepository)
    {
        _mediator = mediator;
        _playerReadRepository = playerReadRepository;
    }

    public async Task<Unit> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
    {
        var user = await _playerReadRepository.GetByIdAsync(request.PlayerId);

        if (user == null)
            throw new NotFoundException(PlayerError.PlayerNotFound);

        var command = new CancelMatchByPlayerCommand(request.PlayerId, request.MatchId);

        return await _mediator.Send(command);
    }
}
