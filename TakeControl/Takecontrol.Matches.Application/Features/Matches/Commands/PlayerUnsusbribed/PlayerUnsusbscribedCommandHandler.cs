using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchesInfo;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.PlayerUnsusbribed;

public sealed class PlayerUnsusbscribedCommandHandler : ICommandHandler<PlayerUnsusbscribed, Unit>
{
    //private readonly IMediator _mediator;
    private readonly IMatchInfoWriteRepository _matchInfoWriteRepository;
    private readonly IMatchInfoReadRepository _matchInfoReadRepository;

    public PlayerUnsusbscribedCommandHandler(IMediator mediator, IMatchInfoWriteRepository matchRepository, IMatchInfoReadRepository matchInfoReadRepository)
    {
        //_mediator = mediator;
        _matchInfoWriteRepository = matchRepository;
        _matchInfoReadRepository = matchInfoReadRepository;
    }

    public async Task<Unit> Handle(PlayerUnsusbscribed request, CancellationToken cancellationToken)
    {
        var matchInfo = await _matchInfoReadRepository.GetMatchInfoByIdAsync(request.MatchId);
        matchInfo.RemovePlayerName(request.PlayerName);

        await _matchInfoWriteRepository.UpdateAsync(matchInfo.MatchId, matchInfo, cancellationToken);
        //TODO: Send notification that player was unsubscribed.
       //await _mediator.Send(emailRequest);
        return Unit.Value;
    }
}
