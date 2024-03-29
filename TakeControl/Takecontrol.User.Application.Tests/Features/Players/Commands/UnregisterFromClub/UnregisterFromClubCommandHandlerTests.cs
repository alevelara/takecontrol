﻿using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.UnregisterFromClub;

[Trait("Category", Category.UnitTest)]
public class UnregisterFromClubCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IPlayerReadRepository> _playerReadRepository;
    private readonly Mock<IPlayerClubReadRepository> _playerClubReadRepository;
    private readonly Mock<IClubReadRepository> _clubReadRepository;

    public UnregisterFromClubCommandHandlerTests()
    {
        _unitOfWork = new();
        _playerClubReadRepository = new();
        _playerReadRepository = new();
        _clubReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_registered_yet()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object, _clubReadRepository.Object);
        Player? player = null;
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_fail_when_club_was_not_register_yet()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object, _clubReadRepository.Object);
        Player? player = ApplicationTestData.CreateMidPlayerForTest(command.UserPlayerId);
        Club? club = null;
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);
        _clubReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));

        _playerReadRepository.Verify(c => c.GetPlayerByUserId(It.IsAny<Guid>()), Times.Once);
        _clubReadRepository.Verify(c => c.GetClubByUserId(It.IsAny<Guid>()), Times.Once);
        _playerClubReadRepository.Verify(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_joint_to_the_club()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object, _clubReadRepository.Object);
        Player player = ApplicationTestData.CreateMidPlayerForTest(command.UserPlayerId);
        Club? club = ApplicationTestData.CreateClubWithAddressForTest(command.UserClubId);
        PlayerClub? playerClub = null;
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);
        _clubReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);
        _playerClubReadRepository.Setup(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(playerClub);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));

        _playerReadRepository.Verify(c => c.GetPlayerByUserId(It.IsAny<Guid>()), Times.Once);
        _clubReadRepository.Verify(c => c.GetClubByUserId(It.IsAny<Guid>()), Times.Once);
        _playerClubReadRepository.Verify(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Should_unregister_the_player_when_player_was_previously_registered_to_the_club()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object, _clubReadRepository.Object);
        Player player = ApplicationTestData.CreateMidPlayerForTest(command.UserPlayerId);
        Club? club = ApplicationTestData.CreateClubWithAddressForTest(command.UserClubId);
        PlayerClub playerClub = PlayerClub.Create(command.UserPlayerId, command.UserClubId);
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);
        _clubReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);
        _playerClubReadRepository.Setup(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(playerClub);
        _unitOfWork.Setup(c => c.Repository<PlayerClub>().Delete(It.IsAny<PlayerClub>()));

        var result = await handler.Handle(command, default);
        Assert.Equal(Unit.Value, result);

        _unitOfWork.Verify(c => c.CompleteAsync(), Times.Once());
        _playerReadRepository.Verify(c => c.GetPlayerByUserId(It.IsAny<Guid>()), Times.Once);
        _clubReadRepository.Verify(c => c.GetClubByUserId(It.IsAny<Guid>()), Times.Once);
        _playerClubReadRepository.Verify(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
    }
}
