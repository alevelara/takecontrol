using MediatR;
using Moq;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Courts.Commands.RegisterCourtsByClub;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Messages.Matches;

namespace Takecontrol.Matches.Application.Tests.Features.Courts.Commands.RegisterCourtsByClub;

public class RegisterCourtsByClubCommandHandlerXUnitTests : IClassFixture<RegisterCourtsByClubCommandHandler>
{
    private readonly Mock<IUnitOfWork> _uoW;
    private readonly Mock<IAsyncWriteRepository<Court>> _courtRepository;
    private readonly Mock<IAsyncWriteRepository<Reservation>> _reservationRepository;


    public RegisterCourtsByClubCommandHandlerXUnitTests()
    {
        _uoW = new();
        _courtRepository = new();
        _reservationRepository = new();
    }

    [Fact]
    public async Task Handle_Should_CreateSameNumberOfCourts_WhenIsHigherThanZero()
    {
        var numberOfCourts = 3;
        var command = new RegisterCourtsByClubCommand(Guid.NewGuid(), numberOfCourts, new TimeOnly(10, 0), new TimeOnly(12, 0));
        var handler = new RegisterCourtsByClubCommandHandler(_uoW.Object);
        _uoW.Setup(c => c.Repository<Court>()).Returns(_courtRepository.Object);
        _uoW.Setup(c => c.Repository<Reservation>()).Returns(_reservationRepository.Object);

        var result = await handler.Handle(command, default);

        _reservationRepository.Verify(x => x.AddRangeAsync(It.IsAny<List<Reservation>>()), Times.Once);
        _courtRepository.Verify(x => x.AddRangeAsync(It.IsAny<List<Court>>()), Times.Once);
        _uoW.Verify(c => c.CompleteAsync(), Times.Once);

        Assert.IsType<Unit>(result);
    }
}
