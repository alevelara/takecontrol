using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Errors.Match;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CancelMatchByPlayer;

internal sealed class CancelMatchByPlayerCommandHandler : ICommandHandler<CancelMatchByPlayerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatchReadRepository _matchReadRepository;
    private readonly IReservationReadRepository _reservationReadRepository;

    private const int LimitHoursToCancel = 1;

    public CancelMatchByPlayerCommandHandler(IUnitOfWork unitOfWork, IMatchReadRepository matchReadRepository, IReservationReadRepository reservationReadRepository)
    {
        _unitOfWork = unitOfWork;
        _matchReadRepository = matchReadRepository;
        _reservationReadRepository = reservationReadRepository;
    }

    public async Task<Unit> Handle(CancelMatchByPlayerCommand request, CancellationToken cancellationToken)
    {
        var match = await _matchReadRepository.GetByIdAsync(request.MatchId);
        ValidateMatch(match!, request.PlayerId);

        var reservation = await _reservationReadRepository.GetReservationById(match!.ReservationId);
        ValidateReservation(reservation!);

        _unitOfWork.Repository<Match>().Update(match);
        await _unitOfWork.CompleteAsync();

        //TODO: Notify the cancellation to related players
        return Unit.Value;
    }

    private void ValidateMatch(Match match, Guid playerId)
    {
        if (match == null)
            throw new NotFoundException(MatchError.MatchNotFound);

        if (match.UserId != playerId)
            throw new ConflictException(MatchError.MatchCanNotBeCancelledByThisPlayer);
    }

    private void ValidateReservation(Reservation reservation)
    {
        if (reservation == null)
            throw new NotFoundException(ReservationError.ReservationNotFound);

        var reservationDate = reservation.ReservationDate.ToDateTime(reservation.StartDate);

        if (DateTime.Now.AddHours(LimitHoursToCancel) > reservationDate)
            throw new ConflictException(ReservationError.ReservationNotCancellable);
    }
}
