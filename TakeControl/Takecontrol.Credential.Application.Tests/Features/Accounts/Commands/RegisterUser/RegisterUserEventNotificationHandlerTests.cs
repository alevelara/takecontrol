using Moq;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Application.Features.Accounts.Commands.RegisterUser;
using Takecontrol.Credential.Domain.Messages.Identity;
using Takecontrol.Shared.Application.Events.Credentials;
using Takecontrol.Shared.Tests.Constants;
using Xunit;

namespace Takecontrol.Credential.Application.Tests.Features.Accounts.Commands.RegisterUser;

[Trait("Category", Category.UnitTest)]
public class RegisterUserEventNotificationHandlerTests
{
    private readonly Mock<IAuthService> _authService;

    public RegisterUserEventNotificationHandlerTests()
    {
        _authService = new();
    }

    [Fact]
    public async Task Should_register_the_club()
    {
        var notification = new RegisterClubMessageNotification("name", "email", "password");
        var handler = new RegisterUserEventNotificationHandler(_authService.Object);
        _authService.Setup(c => c.Register(It.IsAny<RegistrationRequest>()))
            .ReturnsAsync(Guid.NewGuid());

        var result = await handler.Handle(notification, default!);
        Assert.NotEqual(result, Guid.Empty);
        _authService.Verify(c => c.Register(It.IsAny<RegistrationRequest>()));
    }

    [Fact]
    public async Task Should_register_a_player()
    {
        var notification = new RegisterPlayerMessageNotification("name", "email", "password");
        var handler = new RegisterUserEventNotificationHandler(_authService.Object);
        _authService.Setup(c => c.Register(It.IsAny<RegistrationRequest>()))
            .ReturnsAsync(Guid.NewGuid());

        var result = await handler.Handle(notification, default!);
        Assert.NotEqual(result, Guid.Empty);
        _authService.Verify(c => c.Register(It.IsAny<RegistrationRequest>()));
    }
}
