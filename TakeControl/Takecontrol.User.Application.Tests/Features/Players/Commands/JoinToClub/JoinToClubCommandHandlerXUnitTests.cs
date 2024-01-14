using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Commands.JoinToClub;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Domain.Models.PlayerClubs;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.JoinToClub;

[Trait("Category", Category.UnitTest)]
public class JoinToClubCommandHandlerXUnitTests
{
    private readonly Mock<IClubReadRepository> _clubReadRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IPlayerReadRepository> _playerReadRepository;

    public JoinToClubCommandHandlerXUnitTests()
    {
        _clubReadRepository = new();
        _unitOfWork = new();
        _playerReadRepository = new();
    }

    [Fact]
    public async Task Handle_Should_CreateNewAssociation_WhenClubCodeIsCorrect()
    {
        //Arrange
        var code = "12345";
        var command = new JoinToClubCommand(Guid.NewGuid(), Guid.NewGuid(), code);
        var handler = new JoinToClubCommandHandler(_clubReadRepository.Object, _unitOfWork.Object, _playerReadRepository.Object);
        var club = ApplicationTestData.CreateClubForTest(Guid.NewGuid(), ApplicationTestData.CreateAddresForTest());
        var player = ApplicationTestData.CreateMidPlayerForTest(command.UserPlayerId);
        var playerClubWriteRepository = new Mock<IAsyncWriteRepository<PlayerClub>>();

        _clubReadRepository.Setup(c => c.GetClubByCodeAndUserId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(club);
        _playerReadRepository.Setup(p => p.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);
        _unitOfWork.Setup(c => c.Repository<PlayerClub>())
            .Returns(playerClubWriteRepository.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        _unitOfWork.Verify(c => c.Repository<PlayerClub>(), Times.Once);
        _unitOfWork.Verify(c => c.CompleteAsync(), Times.Once);
        _clubReadRepository.Verify(c => c.GetClubByCodeAndUserId(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        _playerReadRepository.Verify(c => c.GetPlayerByUserId(It.IsAny<Guid>()), Times.Once);
        Assert.IsType<Unit>(result);
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_previously_register()
    {
        //Arrange
        var code = "12345";
        var command = new JoinToClubCommand(Guid.NewGuid(), Guid.NewGuid(), code);
        var handler = new JoinToClubCommandHandler(_clubReadRepository.Object, _unitOfWork.Object, _playerReadRepository.Object);
        var club = ApplicationTestData.CreateClubForTest(Guid.NewGuid(), ApplicationTestData.CreateAddresForTest());
        Player? player = null;
        _clubReadRepository.Setup(c => c.GetClubByCodeAndUserId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(club);
        _playerReadRepository.Setup(p => p.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
        _clubReadRepository.Verify(c => c.GetClubByCodeAndUserId(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        _playerReadRepository.Verify(c => c.GetPlayerByUserId(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ThrownConflictException_WhenClubCodeIsInCorrect()
    {
        //Arrange
        var code = "12345";
        var command = new JoinToClubCommand(Guid.NewGuid(), Guid.NewGuid(), code);
        var handler = new JoinToClubCommandHandler(_clubReadRepository.Object, _unitOfWork.Object, _playerReadRepository.Object);

        _clubReadRepository.Setup(c => c.GetClubByCodeAndUserId(It.IsAny<Guid>(), It.IsAny<string>()));

        //Assert
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, default));
        _clubReadRepository.Verify(c => c.GetClubByCodeAndUserId(It.IsAny<Guid>(), It.IsAny<string>()), Times.Once);
        _playerReadRepository.Verify(c => c.GetPlayerByUserId(It.IsAny<Guid>()), Times.Never);
    }
}
