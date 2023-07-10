using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Errors.Match;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.JoinToMatch;

public sealed class JoinToMatchCommandHandler : ICommandHandler<JoinToMatchCommand, Unit>
{
    private readonly IUnitOfWork _uow;
    private readonly IMatchReadRepository _matchReadRepository;
    private readonly IMatchPlayerReadRepository _matchPlayerReadRepository;
    private const int MaxNumberOfPlayersBeforeClosingAMatch = 3;

    public JoinToMatchCommandHandler(IUnitOfWork uow, IMatchReadRepository matchReadRepository, IMatchPlayerReadRepository matchPlayerReadRepository)
    {
        _uow = uow;
        _matchReadRepository = matchReadRepository;
        _matchPlayerReadRepository = matchPlayerReadRepository;
    }

    public async Task<Unit> Handle(JoinToMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _matchReadRepository.GetByIdAsync(request.MatchId);
        ValidateMatch(match!);

        var matchPlayerIndb = await _matchPlayerReadRepository.GetMatchPlayerByPlayerIdAndMatchId(request.PlayerId, request.MatchId);
        if (matchPlayerIndb != null)
        {
            throw new ConflictException(MatchPlayerError.PlayerAlreadyRegistered);
        }

        var listOfPlayersInAMatch = await _matchPlayerReadRepository.GetMatchPlayersByMatchId(request.MatchId);
        if (listOfPlayersInAMatch.Count == MaxNumberOfPlayersBeforeClosingAMatch)
        {
            match!.Close();
            _uow.Repository<Match>().Update(match!);
        }

        var matchPlayer = MatchPlayer.Create(request.MatchId, request.PlayerId);
        await _uow.Repository<MatchPlayer>().AddAsync(matchPlayer);
        await _uow.CompleteAsync();

        return Unit.Value;
    }

    private void ValidateMatch(Match match)
    {
        if (match == null)
        {
            throw new NotFoundException(MatchError.MatchNotFound);
        }

        if (match.IsClosed)
        {
            throw new ConflictException(MatchError.MatchCompleted);
        }
    }
}
