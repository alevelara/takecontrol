using Microsoft.Extensions.Logging;
using Moq;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.Application.Features.Players.Commands.RegisterPlayer;
using takecontrol.Application.Services.Emails;
using takecontrol.Application.Tests.TestsData;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Tests.Features.Players.Commands.RegisterPlayer;

[Trait("Category", "UnitTests")]
public class RegisterPlayerCommandHandlerXUnitTests
{
    private Mock<IUnitOfWork> _uoW;
    private Mock<IAuthService> _authService;
    private Mock<ILogger<RegisterPlayerCommandHandler>> _logger;
    private Mock<ISendEmailService> _emailSender;

    public RegisterPlayerCommandHandlerXUnitTests()
    {
        _uoW = new();
        _authService = new();
        _logger = new();
        _emailSender = new();
    }

    [Fact]
    public async Task Handle_Should_RegisterThePlayer_WhenRegisterPlayerCommandIsValid()
    {
        //Arrange   
        var command = new RegisterPlayerCommand("name", "email@test.com", "Password123!", 1, 1, 1);
        var userId = Guid.NewGuid();
        var player = ApplicationTestData.CreateBegginerPlayerForTest(userId);

        var handler = new RegisterPlayerCommandHandler(_uoW.Object, _authService.Object, _logger.Object, _emailSender.Object);
        var playerRepo = new Mock<IAsyncWriteRepository<Player>>();
        _uoW.Setup(c => c.Repository<Player>()).Returns(playerRepo.Object);

        //Act
        await handler.Handle(command, default);
        _authService.Setup(x => x.Register(It.IsAny<RegistrationRequest>())).ReturnsAsync(userId);
        playerRepo.Setup(c => c.AddAsync(It.IsAny<Player>())).ReturnsAsync(player);
        _uoW.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        //Asserts
        Assert.NotNull(player);
        Assert.Equal(player.UserId, userId);
    }



}
