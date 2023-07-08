using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Features.Players.Commands.RegisterPlayer;
using Takecontrol.User.Infrastructure.Repositories.Primitives;
using Takecontrol.User.Infrastructure.Tests.Mocks;
using Xunit;
using Xunit.Priority;

namespace Takecontrol.Infrastructure.IntegrationTests.Features.Players.Commands.RegisterPlayer;

[Trait("Category", Category.UserIntegratioTests)]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class RegisterPlayerCommandHandlerXUnitTests
{
    private readonly Mock<UnitOfWork> _uoW;
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<ILogger<RegisterPlayerCommandHandler>> _logger;

    public RegisterPlayerCommandHandlerXUnitTests()
    {
        _uoW = MockUnitOfWork.GetUnitOfWork();
        _mediator = new();
        _logger = new();
    }

    [Fact]
    [Priority(-10)]
    public async Task RegisterPlayer_Should_ReturnUnitValue_WhenRequestIsOK()
    {
        var userId = Guid.NewGuid();
        var command = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, 1);
        var handler = new RegisterPlayerCommandHandler(_uoW.Object, _mediator.Object, _logger.Object);

        _mediator.Setup(x => x.Send(It.IsAny<RegisterPlayerMessageNotification>(), default)).ReturnsAsync(userId);
        _mediator.Setup(x => x.Send(It.IsAny<SendWelcomeEmailMessageNotification>(), default));

        var result = await handler.Handle(command, default);
        Assert.IsType<Unit>(result);
    }
}
