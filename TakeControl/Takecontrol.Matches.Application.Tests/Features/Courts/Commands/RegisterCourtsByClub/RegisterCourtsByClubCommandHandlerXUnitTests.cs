using MediatR;
using Moq;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Tests.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandHandlerXUnitTests
{
    private readonly Mock<IUnitOfWork> _uoW;

    public RegisterCourtsByClubCommandHandlerXUnitTests()
    {
        _uoW = new();
    }

    [Fact]
    public async Task Handle_Should_CreateSameNumberOfCourts_WhenIsHigherThanZero()
    {
        var numberOfCourts = 3;
        var command = new RegisterCourtsByClubCommand(Guid.NewGuid(), numberOfCourts);
        var handler = new RegisterCourtsByClubCommandHandler(_uoW.Object);
        var courtsRepo = new Mock<IAsyncWriteRepository<Court>>();
        _uoW.Setup(c => c.Repository<Court>()).Returns(courtsRepo.Object);

        var result = await handler.Handle(command, default);

        Assert.IsType<Unit>(result);
    }
}
