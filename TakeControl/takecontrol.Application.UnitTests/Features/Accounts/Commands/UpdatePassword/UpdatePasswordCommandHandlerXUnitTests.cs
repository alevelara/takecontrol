﻿using MediatR;
using Moq;
using Takecontrol.Application.Contracts.Identity;
using Takecontrol.Application.Features.Accounts.Commands.UpdatePassword;

namespace Takecontrol.Application.UnitTests.Features.Accounts.Commands.UpdatePassword;

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
