﻿using MediatR;
using Moq;
using Takecontrol.Credential.Application.Contracts.Identity;
using Takecontrol.Credential.Application.Features.Accounts.Commands.ResetPassword;
using Takecontrol.Shared.Tests.Constants;
using Xunit;

namespace Takecontrol.Credential.Application.Tests.Features.Accounts.Commands.ResetPassword;

[Trait("Category", Category.UnitTest)]
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
