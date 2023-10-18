using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Errors.Match;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;

public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReservationReadRepository _reservationReadRepository;

    public CreateMatchCommandHandler(IUnitOfWork unitOfWork, IReservationReadRepository reservationReadRepository)
    {
        _unitOfWork = unitOfWork;
        _reservationReadRepository = reservationReadRepository;
    }

    public async Task<Unit> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationReadRepository.GetReservationById(request.ReservationId);

        if (reservation == null)
        {
            throw new NotFoundException(ReservationError.ReservationNotFound);
        }

        if (!reservation.IsAvailable)
        {
            throw new ConflictException(ReservationError.ReservationIsNotAvailable);
        }

        var match = Match.Create(request.ReservationId, request.PlayerId);
        var matchPlayer = MatchPlayer.Create(match.Id, request.PlayerId);
        await _unitOfWork.Repository<Match>().AddAsync(match);
        await _unitOfWork.Repository<MatchPlayer>().AddAsync(matchPlayer);

        reservation.SetIsAvailable(false);
        _unitOfWork.Repository<Reservation>().Update(reservation);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
