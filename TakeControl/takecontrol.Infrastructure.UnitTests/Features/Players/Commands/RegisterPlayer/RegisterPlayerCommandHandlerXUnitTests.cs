using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Infrastructure.IntegrationTests.Mocks;
using takecontrol.Infrastructure.Repositories.Primitives;
using Xunit.Priority;

namespace takecontrol.Infrastructure.IntegrationTests.Features.Players.Commands.RegisterPlayer;

[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class RegisterPlayerCommandHandlerXUnitTests
{

    private readonly Mock<UnitOfWork> _uow;
    private readonly Mock<IAuthService> _authService;
    private readonly Mock<ILogger<RegisterPlayerCommandHandler>> _logger;

    public RegisterPlayerCommandHandlerXUnitTests()
    {
        _uow = MockUnitOfWork.GetUnitOfWork();
        _authService = new();
        _logger = new();
    }

    [Fact]
    [Priority(-10)]
    public async Task RegisterPlayer_Should_ReturnUnitValue_WhenRequestIsOK()
    {
        var command = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, 1);
        var handler = new RegisterPlayerCommandHandler(_uow.Object, _authService.Object, _logger.Object);

        _authService.Setup(s => s.Register(It.IsAny<RegistrationRequest>()))
            .ReturnsAsync(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.IsType<Unit>(result);
    }
}
