using MediatR;
using Moq;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Matches.Commands.CreateMatch;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Tests.Constants;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;

namespace Takecontrol.Matches.Application.Tests.Features.Matches.Commands.CreateMatch;

[Trait("Category", Category.UnitTest)]
public class CreateMatchCommandHandlerXUnitTests
{
    private readonly Mock<IUnitOfWork> _uoW;
    private readonly Mock<IAsyncWriteRepository<Reservation>> _reservationRepository;
    private readonly Mock<IAsyncWriteRepository<Match>> _matchRepository;
    private readonly Mock<IAsyncWriteRepository<MatchPlayer>> _matchPlayerRepository;
    private readonly Mock<IReservationReadRepository> _reservationReadRepository;

    public CreateMatchCommandHandlerXUnitTests()
    {
        _uoW = new();
        _reservationRepository = new();
        _matchRepository = new();
        _matchPlayerRepository = new();
        _reservationReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_reservation_does_not_exists()
    {
        //Arrange
        var command = new CreateMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CreateMatchCommandHandler(_uoW.Object, _reservationReadRepository.Object);
        Reservation? reservation = null;

        //Act
        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_fail_when_reservation_is_not_available()
    {
        //Arrange
        var command = new CreateMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CreateMatchCommandHandler(_uoW.Object, _reservationReadRepository.Object);
        Reservation? reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(10, 0), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.Now));
        reservation.SetIsAvailable(false);

        //Act
        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        //Assert
        await Assert.ThrowsAsync<ConflictException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_success_when_reservation_is_available()
    {
        //Arrange
        var command = new CreateMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CreateMatchCommandHandler(_uoW.Object, _reservationReadRepository.Object);
        Reservation? reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(10, 0), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.Now));
        _uoW.Setup(r => r.Repository<Match>()).Returns(_matchRepository.Object);
        _uoW.Setup(r => r.Repository<MatchPlayer>()).Returns(_matchPlayerRepository.Object);
        _uoW.Setup(r => r.Repository<Reservation>()).Returns(_reservationRepository.Object);

        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        _reservationRepository.Verify(c => c.Update(It.IsAny<Reservation>()), Times.Once);
        _matchRepository.Verify(c => c.AddAsync(It.IsAny<Match>()), Times.Once);
        _matchPlayerRepository.Verify(c => c.AddAsync(It.IsAny<MatchPlayer>()), Times.Once);
        Assert.Equal(Unit.Value, result);
    }
}
