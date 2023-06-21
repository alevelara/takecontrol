using MediatR;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Errors.Match;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;

public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatchReadRepository _matchReadRepository;

    public CreateMatchCommandHandler(IUnitOfWork unitOfWork, IMatchReadRepository matchReadRepository)
    {
        _unitOfWork = unitOfWork;
        _matchReadRepository = matchReadRepository;
    }

    public async Task<Unit> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var reservationIsAlreadyCompleted = await _matchReadRepository.IsThisReservationCompleted(request.ReservationId);
        if (reservationIsAlreadyCompleted)
        {
            throw new ConflictException(MatchError.MatchCreatedInAReservationCompleted);
        }

        var match = Match.Create(request.ReservationId, request.PlayerId);
        var matchPlayer = MatchPlayer.Create(match.Id, request.PlayerId);

        await _unitOfWork.Repository<Match>().AddAsync(match);
        await _unitOfWork.Repository<MatchPlayer>().AddAsync(matchPlayer);
        await _unitOfWork.CompleteAsync();

        //TODO: Update IsAvailable property to false from Reservation entity.

        return Unit.Value;
    }
}
