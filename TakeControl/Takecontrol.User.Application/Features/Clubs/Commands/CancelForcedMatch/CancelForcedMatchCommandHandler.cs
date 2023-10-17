using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Domain.Errors.Clubs;

namespace Takecontrol.User.Application.Features.Clubs.Commands.CancelForcedMatch;

public sealed class CancelForcedMatchCommandHandler : ICommandHandler<CancelForcedMatchCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IClubReadRepository _clubReadRepository;

    public CancelForcedMatchCommandHandler(IMediator mediator, IClubReadRepository clubReadRepository)
    {
        _mediator = mediator;
        _clubReadRepository = clubReadRepository;
    }

    public async Task<Unit> Handle(CancelForcedMatchCommand request, CancellationToken cancellationToken)
    {
        var club = await _clubReadRepository.GetClubByUserId(request.ClubId);
        if (club == null)
            throw new NotFoundException(ClubError.ClubNotFound);

        var command = new CancelForcedMatchByClubCommand(club.Id, request.MatchId, request.Description);
        return await _mediator.Send(command);
    }
}
