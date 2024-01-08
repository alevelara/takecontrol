using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.User.Application.Contracts.Persistence.PlayerClubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Commands.UnregisterFromClub;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.UnregisterFromClub;

public class UnregisterFromClubCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IPlayerReadRepository> _playerReadRepository;
    private readonly Mock<IPlayerClubReadRepository> _playerClubReadRepository;

    public UnregisterFromClubCommandHandlerTests()
    {
        _unitOfWork = new();
        _playerClubReadRepository = new();
        _playerReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_registered_yet()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object);
        Player? player = null;
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_joint_to_the_club()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object);
        Player player = ApplicationTestData.CreateMidPlayerForTest(command.UserId);
        PlayerClub? playerClub = null;
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);
        _playerClubReadRepository.Setup(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(playerClub);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_unregister_the_player_when_player_was_previously_registered_to_the_club()
    {
        var command = new UnregisterFromClubCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new UnregisterFromClubCommandHandler(_unitOfWork.Object, _playerReadRepository.Object, _playerClubReadRepository.Object);
        Player player = ApplicationTestData.CreateMidPlayerForTest(command.UserId);
        PlayerClub playerClub = PlayerClub.Create(command.UserId, command.ClubId);
        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>())).
            ReturnsAsync(player);
        _playerClubReadRepository.Setup(c => c.GetByPlayerIdAndClubId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(playerClub);
        _unitOfWork.Setup(c => c.Repository<PlayerClub>().Delete(It.IsAny<PlayerClub>()));

        var result = await handler.Handle(command, default);
        Assert.Equal(Unit.Value, result);

        _unitOfWork.Verify(c => c.CompleteAsync(), Times.Once());
    }
}
