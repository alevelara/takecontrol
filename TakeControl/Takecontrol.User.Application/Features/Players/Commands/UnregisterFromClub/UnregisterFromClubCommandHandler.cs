using MediatR;
using Takecontrol.Shared.Application.Abstractions.Mediatr;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Errors.Clubs;
using Takecontrol.User.Domain.Errors.PlayerClubs;
using Takecontrol.User.Domain.Errors.Players;
using Takecontrol.User.Domain.Models.PlayerClubs;

namespace Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;

public sealed class UnregisterFromClubCommandHandler : ICommandHandler<UnregisterFromClubCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPlayerReadRepository _playerReadRepository;
    private readonly IClubReadRepository _clubReadRepository;
    private readonly IPlayerClubReadRepository _playerClubReadRepository;
    public UnregisterFromClubCommandHandler(IUnitOfWork unitOfWork, IPlayerReadRepository playerReadRepository, IPlayerClubReadRepository playerClubReadRepository, IClubReadRepository clubReadRepository)
    {
        _unitOfWork = unitOfWork;
        _playerReadRepository = playerReadRepository;
        _playerClubReadRepository = playerClubReadRepository;
        _clubReadRepository = clubReadRepository;
    }

    public async Task<Unit> Handle(UnregisterFromClubCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerReadRepository.GetPlayerByUserId(request.UserPlayerId);
        if (player == null)
            throw new NotFoundException(PlayerError.PlayerNotFound);

        var club = await _clubReadRepository.GetClubByUserId(request.UserClubId);
        if (club == null)
            throw new NotFoundException(ClubError.ClubNotFound);

        var playerClub = await _playerClubReadRepository.GetByPlayerIdAndClubId(player.Id, club.Id);
        if (playerClub == null)
            throw new NotFoundException(PlayerClubError.PlayerDoesntBelongToTheClub);

        _unitOfWork.Repository<PlayerClub>().Delete(playerClub);
        await _unitOfWork.CompleteAsync();
        return Unit.Value;
    }
}
