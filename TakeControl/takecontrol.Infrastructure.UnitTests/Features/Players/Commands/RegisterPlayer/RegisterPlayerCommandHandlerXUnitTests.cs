using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Takecontrol.Application.Contracts.Identity;
using Takecontrol.Application.Contracts.Persitence.Primitives;
using Takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using Takecontrol.Application.Services.Emails;
using Takecontrol.Domain.Messages.Identity;
using Takecontrol.Domain.Models.Emails;
using Takecontrol.Infrastructure.IntegrationTests.Mocks;
using Takecontrol.Infrastructure.Repositories.Primitives;
using Xunit.Priority;

namespace Takecontrol.Infrastructure.IntegrationTests.Features.Players.Commands.RegisterPlayer;

[Trait("Category", "InfrastructureIntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class RegisterPlayerCommandHandlerXUnitTests
{
    private readonly Mock<UnitOfWork> _uow;
    private readonly Mock<IAuthService> _authService;
    private readonly Mock<ILogger<RegisterPlayerCommandHandler>> _logger;
    private readonly Mock<ISendEmailService> _emailSender;

    public RegisterPlayerCommandHandlerXUnitTests()
    {
        _uow = MockUnitOfWork.GetUnitOfWork();
        _authService = new();
        _logger = new();
        _emailSender = new();
    }

    [Fact]
    [Priority(-10)]
    public async Task RegisterPlayer_Should_ReturnUnitValue_WhenRequestIsOK()
    {
        var command = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, 1);
        var handler = new RegisterPlayerCommandHandler(_uow.Object, _authService.Object, _logger.Object, _emailSender.Object);

        _authService.Setup(s => s.Register(It.IsAny<RegistrationRequest>()))
            .ReturnsAsync(Guid.NewGuid());

        _emailSender.Setup(e => e.SendEmailAsync(It.IsAny<Email>(), default(CancellationToken)));

        var result = await handler.Handle(command, default);
        Assert.IsType<Unit>(result);
    }
}
