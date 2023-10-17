using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Errors.Match;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CancelForcedMatchByClub;

public sealed class CancelForcedMatchByClubCommandHandler : ICommandHandler<CancelForcedMatchByClubCommand, Unit>
{
    private readonly IMatchReadRepository _matchReadRepository;
    private readonly IMatchPlayerReadRepository _matchPlayerReadRepository;
    private readonly IReservationReadRepository _reservationReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelForcedMatchByClubCommandHandler(IMatchReadRepository matchReadRepository, IReservationReadRepository reservationReadRepository, IMatchPlayerReadRepository matchPlayerReadRepository, IUnitOfWork unitOfWork)
    {
        _matchReadRepository = matchReadRepository;
        _reservationReadRepository = reservationReadRepository;
        _unitOfWork = unitOfWork;
        _matchPlayerReadRepository = matchPlayerReadRepository;
    }

    public async Task<Unit> Handle(CancelForcedMatchByClubCommand request, CancellationToken cancellationToken)
    {
        var match = await _matchReadRepository.GetByIdAsync(request.MatchId);
        ValidateMatch(match!);
        var reservation = await _reservationReadRepository.GetReservationById(match!.ReservationId);
        ValidateReservation(reservation!);

        var players = await _matchPlayerReadRepository.GetMatchPlayersByMatchId(request.MatchId);
        if (reservation!.Court.ClubId.Equals(request.ClubId))
            throw new ConflictException(MatchError.MatchCanNotBeCancelledByThisClub);

        match.Cancel(request.Description);
        _unitOfWork.Repository<Match>().Update(match);
        await _unitOfWork.CompleteAsync();

        //TODO: Notify to the related players -> Remove players.Clear()
        players.Clear();
        return Unit.Value;
    }

    private void ValidateMatch(Match match)
    {
        if (match == null)
            throw new NotFoundException(MatchError.MatchNotFound);
    }

    private void ValidateReservation(Reservation reservation)
    {
        if (reservation == null)
            throw new NotFoundException(ReservationError.ReservationNotFound);

        if (reservation.Court == null)
            throw new NotFoundException(CourtError.CourtNotFound);
    }
}