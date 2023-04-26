using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Errors.Clubs;
using Takecontrol.User.Domain.Models.PlayerClubs;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToClub;

public sealed class JoinToClubCommandHandler : ICommandHandler<JoinToClubCommand, Unit>
{
    private readonly IClubReadRepository _clubReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinToClubCommandHandler(IClubReadRepository clubReadRepository, IUnitOfWork unitOfWork)
    {
        _clubReadRepository = clubReadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(JoinToClubCommand request, CancellationToken cancellationToken)
    {
        var club = await _clubReadRepository.GetClubByCodeAndClubId(request.ClubId, request.Code);

        if (club == null)
            throw new ConflictException(ClubError.ClubDoesnotMatchByCode);

        var association = PlayerClub.Create(request.PlayerId, request.ClubId);

        await _unitOfWork.Repository<PlayerClub>().AddAsync(association);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
