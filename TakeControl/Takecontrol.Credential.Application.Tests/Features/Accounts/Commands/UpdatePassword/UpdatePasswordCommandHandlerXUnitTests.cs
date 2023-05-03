using MediatR;
using Moq;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Application.Features.Accounts.Commands.UpdatePassword;
using Xunit;

namespace Takecontrol.Credential.Application.Tests.Features.Accounts.Commands.UpdatePassword;

[Trait("Category", "UnitTests")]
public class UpdatePasswordCommandHandlerXUnitTests
{
    private readonly Mock<IAuthService> _authService;

    public UpdatePasswordCommandHandlerXUnitTests()
    {
        _authService = new();
    }

    [Fact]
    public async Task Handle_Should_ResetPassword_WhenRequestIsValid()
    {
        var command = new UpdatePasswordCommand("email@test.com", "Newpass12!");
        var handler = new UpdatePasswordCommandHandler(_authService.Object);
        _authService.Setup(x => x.UpdatePassword(It.IsAny<UpdatePasswordCommand>()))
            .ReturnsAsync(true);

        var result = await handler.Handle(command, default);

        Assert.IsType<Unit>(result);
    }
}
