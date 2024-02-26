using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchesInfo;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.PlayerSubscribed;

public sealed class PlayerSubscribedCommandHandler : ICommandHandler<PlayerSusbscribed, Unit>
{
    private readonly IMatchInfoWriteRepository _matchInfoWriteRepository;
    private readonly IMatchInfoReadRepository _matchInfoReadRepository;

    public PlayerSubscribedCommandHandler(IMatchInfoWriteRepository matchInfoWriteRepository, IMatchInfoReadRepository matchInfoReadRepository)
    {
        _matchInfoWriteRepository = matchInfoWriteRepository;
        _matchInfoReadRepository = matchInfoReadRepository;
    }

    public async Task<Unit> Handle(PlayerSusbscribed request, CancellationToken cancellationToken)
    {
        var matchInfo = await _matchInfoReadRepository.GetMatchInfoByIdAsync(request.MatchId);
        matchInfo.AddPlayerName(request.PlayerName);

        await _matchInfoWriteRepository.UpdateAsync(matchInfo.MatchId, matchInfo, cancellationToken);
        //TODO: Send notification that player was subscribed to the match.
        //await _mediator.Send(emailRequest);
        return Unit.Value;
    }
}
