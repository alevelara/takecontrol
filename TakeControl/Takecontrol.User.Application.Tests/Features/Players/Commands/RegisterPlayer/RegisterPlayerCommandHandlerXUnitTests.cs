using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.User.Application.Features.Players.Commands.RegisterPlayer;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.RegisterPlayer;

[Trait("Category", "UnitTests")]
public class RegisterPlayerCommandHandlerXUnitTests
{
    private readonly Mock<IUnitOfWork> _uoW;
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<ILogger<RegisterPlayerCommandHandler>> _logger;

    public RegisterPlayerCommandHandlerXUnitTests()
    {
        _uoW = new();
        _mediator = new();
        _logger = new();
    }

    [Fact]
    public async Task Handle_Should_RegisterThePlayer_WhenRegisterPlayerCommandIsValid()
    {
        //Arrange
        var command = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, 1);
        var userId = Guid.NewGuid();
        var player = ApplicationTestData.CreateBegginerPlayerForTest(userId);

        var handler = new RegisterPlayerCommandHandler(_uoW.Object, _mediator.Object, _logger.Object);
        var playerRepo = new Mock<IAsyncWriteRepository<Player>>();
        _uoW.Setup(c => c.Repository<Player>()).Returns(playerRepo.Object);

        //Act
        await handler.Handle(command, default);
        _mediator.Setup(x => x.Send(It.IsAny<RegisterPlayerMessageNotification>(), default)).ReturnsAsync(userId);
        _mediator.Setup(x => x.Publish(It.IsAny<SendWelcomeEmailMessageNotification>(), default)); playerRepo.Setup(c => c.AddAsync(It.IsAny<Player>())).ReturnsAsync(player);
        _uoW.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        //Asserts
        Assert.NotNull(player);
        Assert.Equal(player.UserId, userId);
    }
}
