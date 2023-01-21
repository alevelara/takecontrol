using Microsoft.Extensions.Logging;
using Moq;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Application.Services.Emails;
using takecontrol.Application.Tests.TestsData;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.Tests.Features.Clubs.Commands.RegisterClub;

[Trait("Category", "UnitTests")]
public class RegisterClubCommandHandlerXUnitTests
{
    private Mock<IUnitOfWork> _uoW;
    private Mock<IAuthService> _authService;
    private Mock<ILogger<RegisterClubCommandHandler>> _logger;
    private Mock<ISendEmailService> _emailService;

    public RegisterClubCommandHandlerXUnitTests()
    {
        _uoW = new();
        _authService = new();
        _logger = new();
        _emailService = new();
    }

    [Fact]
    public async Task Handle_Should_RegisterTheClub_WhenRegisterClubCommandIsValid()
    {
        //Arrange   
        var command = new RegisterClubCommand("name", "city", "province", "mainAddress", "email", "password");
        var userId = Guid.NewGuid();
        var address = ApplicationTestData.CreateAddresForTest();
        var club = ApplicationTestData.CreateClubForTest(userId, address);

        var handler = new RegisterClubCommandHandler(_uoW.Object, _authService.Object, _logger.Object, _emailService.Object);
        var addressRepo = new Mock<IAsyncWriteRepository<Address>>();
        _uoW.Setup(c => c.Repository<Address>()).Returns(addressRepo.Object);
        var clubRepo = new Mock<IAsyncWriteRepository<Club>>();
        _uoW.Setup(c => c.Repository<Club>()).Returns(clubRepo.Object);

        //Act
        await handler.Handle(command, default);
        _authService.Setup(x => x.Register(It.IsAny<RegistrationRequest>())).ReturnsAsync(userId);
        addressRepo.Setup(a => a.AddAsync(It.IsAny<Address>())).ReturnsAsync(address);
        clubRepo.Setup(c => c.AddAsync(It.IsAny<Club>())).ReturnsAsync(club);
        _uoW.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

        //Asserts
        Assert.NotNull(club);
        Assert.Equal(club.AddresId, address.Id);
        Assert.Equal(club.UserId, userId);
    }
}
