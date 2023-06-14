using MediatR;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Shared.Application.Abstractions.Mediatr;

namespace Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;

public class CreateMatchCommandHandler : ICommandHandler<CreateMatchCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMatchCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
    {
        var match = Match.Create(request.ReservationId, request.PlayerId);
        var matchPlayer = MatchPlayer.Create(match.Id, request.PlayerId);
        
        await _unitOfWork.Repository<Match>().AddAsync(match);
        await _unitOfWork.Repository<MatchPlayer>().AddAsync(matchPlayer);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
