using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Errors.Clubs;
using Takecontrol.User.Domain.Errors.Players;
using Takecontrol.User.Domain.Models.PlayerClubs;

namespace Takecontrol.User.Application.Features.Players.Commands.JoinToClub;

public sealed class JoinToClubCommandHandler : ICommandHandler<JoinToClubCommand, Unit>
{
    private readonly IClubReadRepository _clubReadRepository;
    private readonly IPlayerReadRepository _playerReadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JoinToClubCommandHandler(IClubReadRepository clubReadRepository, IUnitOfWork unitOfWork, IPlayerReadRepository playerReadRepository)
    {
        _clubReadRepository = clubReadRepository;
        _unitOfWork = unitOfWork;
        _playerReadRepository = playerReadRepository;
    }

    public async Task<Unit> Handle(JoinToClubCommand request, CancellationToken cancellationToken)
    {
        var club = await _clubReadRepository.GetClubByCodeAndUserId(request.UserClubId, request.Code);
        if (club == null)
            throw new ConflictException(ClubError.ClubDoesnotMatchByCode);

        var player = await _playerReadRepository.GetPlayerByUserId(request.UserPlayerId);
        if (player == null)
            throw new NotFoundException(PlayerError.PlayerNotFound);

        var association = PlayerClub.Create(player!.Id, club!.Id);

        await _unitOfWork.Repository<PlayerClub>().AddAsync(association);
        await _unitOfWork.CompleteAsync();

        return Unit.Value;
    }
}
