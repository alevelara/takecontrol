using MediatR;
using Moq;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Features.Accounts.Commands.ResetPassword;

namespace takecontrol.Application.UnitTests.Features.Accounts.Commands.ResetPassword;

[Trait("Category", "UnitTests")]
public class ResetPasswordCommandHandlerXUnitTests
{
    private readonly Mock<IAuthService> _authService;

    public ResetPasswordCommandHandlerXUnitTests()
    {
        _authService = new();
    }

    [Fact]
    public async Task Handle_Should_ResetPassword_WhenRequestIsValid()
    {
        var command = new ResetPasswordCommand("email@test.com", "Password123!", "Newpass12!");
        var handler = new ResetPasswordCommandHandler(_authService.Object);
        _authService.Setup(x => x.ResetPassword(It.IsAny<ResetPasswordCommand>()))
            .ReturnsAsync(true);

        var result = await handler.Handle(command, default);
        Assert.IsType<Unit>(result);
    }
}
