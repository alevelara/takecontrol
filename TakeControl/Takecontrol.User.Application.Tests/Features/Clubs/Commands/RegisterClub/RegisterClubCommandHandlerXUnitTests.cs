using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Application.Events.Emails;
using Takecontrol.User.Application.Features.Clubs.Commands.RegisterClub;
using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Clubs.Commands.RegisterClub;

[Trait("Category", "UnitTests")]
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
    public async Task Handle_Should_RegisterTheClub_WhenRegisterClubCommandIsValid()
    {
        //Arrange
        var command = new RegisterClubCommand("name", "city", "province", "mainAddress", "email", "password");
        var userId = Guid.NewGuid();
        var address = ApplicationTestData.CreateAddresForTest();
        var club = ApplicationTestData.CreateClubForTest(userId, address);

        var handler = new RegisterClubCommandHandler(_uoW.Object, _mediator.Object, _logger.Object);
        var addressRepo = new Mock<IAsyncWriteRepository<Address>>();
        _uoW.Setup(c => c.Repository<Address>()).Returns(addressRepo.Object);
        var clubRepo = new Mock<IAsyncWriteRepository<Club>>();
        _uoW.Setup(c => c.Repository<Club>()).Returns(clubRepo.Object);

        //Act
        await handler.Handle(command, default);
        _mediator.Setup(x => x.Send(It.IsAny<RegisterClubMessageNotification>(), default)).ReturnsAsync(userId);
        _mediator.Setup(x => x.Send(It.IsAny<SendWelcomeEmailMessageNotification>(), default));
        addressRepo.Setup(a => a.AddAsync(It.IsAny<Address>())).ReturnsAsync(address);
        clubRepo.Setup(c => c.AddAsync(It.IsAny<Club>())).ReturnsAsync(club);
        _uoW.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        //Asserts
        Assert.NotNull(club);
        Assert.Equal(club.AddresId, address.Id);
        Assert.Equal(club.UserId, userId);
    }
}
