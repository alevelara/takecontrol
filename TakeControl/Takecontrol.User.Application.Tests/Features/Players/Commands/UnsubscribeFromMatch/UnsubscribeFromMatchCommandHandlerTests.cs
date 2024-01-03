using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Commands.UnsubscribeFromMatch;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.UnsubscribeFromMatch;

[Trait("Category", Category.UnitTest)]
public class UnsubscribeFromMatchCommandHandlerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IPlayerReadRepository> _playerReadRepository;

    public UnsubscribeFromMatchCommandHandlerTests()
    {
        _mediator = new();
        _playerReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_registered_previously()
    {
        Player? player = null;
        var handler = new UnsubscribeFromMatchCommandHandler(_mediator.Object, _playerReadRepository.Object);
        var request = new UnsubscribeFromMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        _playerReadRepository.Setup(s => s.GetPlayerByUserId(request.UserId))
            .ReturnsAsync(player);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
        _playerReadRepository.Verify(c => c.GetPlayerByUserId(request.UserId), Times.Once());
    }

    [Fact]
    public async Task Should_proceed_if_player_is_correctly_registered()
    {
        //Arrange
        var handler = new UnsubscribeFromMatchCommandHandler(_mediator.Object, _playerReadRepository.Object);
        var request = new UnsubscribeFromMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        Player player = ApplicationTestData.CreateMidPlayerForTest(request.UserId);
        var command = new UnsubscribePlayerFromMatchCommand(player.Id, request.MatchId);

        _playerReadRepository.Setup(s => s.GetPlayerByUserId(request.UserId))
            .ReturnsAsync(player);
        _mediator.Setup(m => m.Send(It.IsAny<UnsubscribePlayerFromMatchCommand>(), default));

        //Act
        var result = await handler.Handle(request, default);

        //Assert
        _playerReadRepository.Verify(c => c.GetPlayerByUserId(request.UserId), Times.Once());
        _mediator.Verify(c => c.Send(command, default), Times.Once());

        Assert.Equal(result, Unit.Value);
    }
}
