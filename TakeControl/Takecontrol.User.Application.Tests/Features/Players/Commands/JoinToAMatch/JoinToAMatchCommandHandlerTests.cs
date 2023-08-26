using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Commands.JoinToAMatch;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.JoinToAMatch;

[Trait("Category", Category.UnitTest)]
public class JoinToAMatchCommandHandlerTests
{
    private readonly Mock<IPlayerReadRepository> _playerReadRepository;
    private readonly Mock<IMediator> _mediator;

    public JoinToAMatchCommandHandlerTests()
    {
        _playerReadRepository = new();
        _mediator = new();
    }

    [Fact]
    public async Task Should_failed_when_player_is_not_registered()
    {
        Player? player = null;
        _playerReadRepository.Setup(x => x.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        var command = new JoinToAMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new JoinToAMatchCommandHandler(_playerReadRepository.Object, _mediator.Object);

        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_send_command_to_match_module_to_join_the_player_to_a_match()
    {
        Player? player = ApplicationTestData.CreateMidPlayerForTest(Guid.NewGuid());
        _playerReadRepository.Setup(x => x.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);
        var command = new JoinToAMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new JoinToAMatchCommandHandler(_playerReadRepository.Object, _mediator.Object);

        var result = await handler.Handle(command, default);

        Assert.Equal(Unit.Value, result);

        _mediator.Verify(c => c.Send(It.IsAny<JoinToMatchCommand>(), default), Times.Once());
    }
}
