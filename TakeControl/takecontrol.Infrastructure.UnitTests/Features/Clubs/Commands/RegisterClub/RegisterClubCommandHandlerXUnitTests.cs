﻿using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using takecontrol.Application.Contracts.Identity;
using takecontrol.Application.Features.Clubs.Commands.RegisterClub;
using takecontrol.Domain.Messages.Identity;
using takecontrol.Infrastructure.IntegrationTests.Mocks;
using takecontrol.Infrastructure.Repositories.Primitives;
using Xunit.Priority;

namespace takecontrol.Infrastructure.IntegrationTests.Features.Clubs.Commands.RegisterClub;

[Trait("Category", "IntegrationTests")]
[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class RegisterClubCommandHandlerXUnitTests
{

    private readonly Mock<UnitOfWork> _uow;
    private readonly Mock<IAuthService> _authService;
    private readonly Mock<ILogger<RegisterClubCommandHandler>> _logger;

    public RegisterClubCommandHandlerXUnitTests()
    {
        _uow = MockUnitOfWork.GetUnitOfWork();
        _authService = new();
        _logger = new();
    }

    [Fact]
    [Priority(-10)]
    public async Task RegisterClub_Should_ReturnUnitValue_WhenRequestIsOK()
    {
        var command = new RegisterClubCommand("name", "city", "province", "mainAddress", "email", "password");
        var handler = new RegisterClubCommandHandler(_uow.Object, _authService.Object, _logger.Object);

        _authService.Setup(s => s.Register(It.IsAny<RegistrationRequest>()))
            .ReturnsAsync(Guid.NewGuid());

        var result = await handler.Handle(command, default);

        Assert.NotNull(result);
        Assert.IsType<Unit>(result);
    }
}
