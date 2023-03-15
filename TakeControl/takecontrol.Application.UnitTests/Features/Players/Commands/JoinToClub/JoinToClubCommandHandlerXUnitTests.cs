﻿using MediatR;
using Moq;
using Takecontrol.Application.Contracts.Persitence.Clubs;
using Takecontrol.Application.Contracts.Persitence.Primitives;
using Takecontrol.Application.Exceptions;
using Takecontrol.Application.Features.Players.Commands.JoinToClub;
using Takecontrol.Application.Tests.TestsData;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Domain.Models.PlayerClubs;

namespace Takecontrol.Application.UnitTests.Features.Players.Commands.JoinToClub;

public class JoinToClubCommandHandlerXUnitTests
{
    private readonly Mock<IClubReadRepository> _clubReadRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public JoinToClubCommandHandlerXUnitTests()
    {
        _clubReadRepository = new();
        _unitOfWork = new();
    }

    [Fact]
    public async Task Handle_Should_CreateNewAssociation_WhenClubCodeIsCorrect()
    {
        //Arrange
        var code = "12345";
        var command = new JoinToClubCommand(Guid.NewGuid(), Guid.NewGuid(), code);
        var handler = new JoinToClubCommandHandler(_clubReadRepository.Object, _unitOfWork.Object);
        var club = ApplicationTestData.CreateClubForTest(Guid.NewGuid(), ApplicationTestData.CreateAddresForTest());
        var playerClubWriteRepository = new Mock<IAsyncWriteRepository<PlayerClub>>();

        _clubReadRepository.Setup(c => c.GetClubByCodeAndClubId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(club);
        _unitOfWork.Setup(c => c.Repository<PlayerClub>())
            .Returns(playerClubWriteRepository.Object);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        _unitOfWork.Verify(c => c.Repository<PlayerClub>(), Times.Once);
        _unitOfWork.Verify(c => c.CompleteAsync(), Times.Once);
        Assert.IsType<Unit>(result);
    }

    [Fact]
    public async Task Handle_Should_ThrownConflictException_WhenClubCodeIsInCorrect()
    {
        //Arrange
        var code = "12345";
        var command = new JoinToClubCommand(Guid.NewGuid(), Guid.NewGuid(), code);
        var handler = new JoinToClubCommandHandler(_clubReadRepository.Object, _unitOfWork.Object);
        Club club = null;

        _clubReadRepository.Setup(c => c.GetClubByCodeAndClubId(It.IsAny<Guid>(), It.IsAny<string>()))
            .ReturnsAsync(club);

        //Assert
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, default));
    }
}
