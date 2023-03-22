using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;
using Xunit;
using Xunit.Priority;

namespace Takecontrol.User.Infrastructure.Tests.Features.Clubs.Commands.RegisterClub;

[Trait("Category", "InfrastructureIntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class RegisterClubCommandHandlerXUnitTests
{
    private readonly Mock<IUnitOfWork> _uoW;
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<ILogger<RegisterClubCommandHandler>> _logger;

    public RegisterClubCommandHandlerXUnitTests()
    {
        _uoW = new();
        _mediator = new();
        _logger = new();
    }

    [Fact]
    [Priority(-10)]
    public async Task RegisterClub_Should_ReturnUnitValue_WhenRequestIsOK()
    {
        var userId = Guid.NewGuid();
        var command = new RegisterClubCommand("name", "city", "province", "mainAddress", "email", "password");
        var handler = new RegisterClubCommandHandler(_uoW.Object, _mediator.Object, _logger.Object);

        _mediator.Setup(x => x.Send(It.IsAny<RegisterClubMessageNotification>(), default)).ReturnsAsync(userId);
        _mediator.Setup(x => x.Send(It.IsAny<SendWelcomeEmailMessageNotification>(), default));

        var result = await handler.Handle(command, default);
        Assert.IsType<Unit>(result);
    }
}
