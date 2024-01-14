using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Commands.CancelMatch;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.CancelMatch;

[Trait("Category", Category.UnitTest)]
public class CancelMatchCommandHandlerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IPlayerReadRepository> _playerReadRepository;

    public CancelMatchCommandHandlerTests()
    {
        _mediator = new();
        _playerReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_user_does_not_exist()
    {
        var command = new CancelMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CancelMatchCommandHandler(_mediator.Object, _playerReadRepository.Object);
        Player player = null;

        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_cancel_the_match_successfully()
    {
        var command = new CancelMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CancelMatchCommandHandler(_mediator.Object, _playerReadRepository.Object);
        Player player = ApplicationTestData.CreateMidPlayerForTest(Guid.NewGuid());
        var commandToSend = new CancelMatchByPlayerCommand(player.Id, command.MatchId);

        _playerReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        await handler.Handle(command, default!);
        _mediator.Verify(c => c.Send(commandToSend, default!), Times.Once);
    }
}
