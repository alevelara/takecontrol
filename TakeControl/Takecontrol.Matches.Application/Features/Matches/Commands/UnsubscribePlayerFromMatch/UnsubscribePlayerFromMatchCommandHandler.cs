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

namespace Takecontrol.Matches.Application.Features.Matches.Commands.UnsubscribePlayerFromMatch;

public sealed class UnsubscribePlayerFromMatchCommandHandler : ICommandHandler<UnsubscribePlayerFromMatchCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatchReadRepository _matchReadRepository;
    private readonly IMatchPlayerReadRepository _matchPlayerReadRepository;

    public UnsubscribePlayerFromMatchCommandHandler(IUnitOfWork unitOfWork, IMatchReadRepository matchReadRepository, IMatchPlayerReadRepository matchPlayerReadRepository)
    {
        _unitOfWork = unitOfWork;
        _matchReadRepository = matchReadRepository;
        _matchPlayerReadRepository = matchPlayerReadRepository;
    }

    public async Task<Unit> Handle(UnsubscribePlayerFromMatchCommand request, CancellationToken cancellationToken)
    {
        var match = await _matchReadRepository.GetMatchWithReservation(request.MatchId);
        if (match == null)
            throw new NotFoundException(MatchError.MatchNotFound);

        if (match.Reservation.IsCancellableByDate(DateTime.UtcNow.AddDays(-1)))
            throw new ConflictException(MatchError.LimitHourToCancelHasExpired);

        var matchPlayer = await _matchPlayerReadRepository.GetMatchPlayerByPlayerIdAndMatchId(request.PlayerId, request.MatchId);
        if (matchPlayer == null)
            throw new NotFoundException(MatchPlayerError.MatchPlayerNotFound);

        _unitOfWork.Repository<MatchPlayer>().Delete(matchPlayer);

        match.Open();
        _unitOfWork.Repository<Match>().Update(match);
        await _unitOfWork.CompleteAsync();
        return Unit.Value;
    }
}
