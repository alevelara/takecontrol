using Moq;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Matches.Commands.CancelMatchByPlayer;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;

namespace Takecontrol.Matches.Application.Tests.Features.Matches.Commands.CancelMatchByPlayer;

[Trait("Category", Category.UnitTest)]
public class CancelMatchByPlayerCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMatchReadRepository> _matchReadRepository;
    private readonly Mock<IReservationReadRepository> _reservationReadRepository;

    public CancelMatchByPlayerCommandHandlerTests()
    {
        _unitOfWork = new();
        _matchReadRepository = new();
        _reservationReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_match_does_not_exist()
    {
        Match match = null;
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).
            ReturnsAsync(match);

        var command = new CancelMatchByPlayerCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CancelMatchByPlayerCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _reservationReadRepository.Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_fail_when_other_player_tries_to_cancel_the_match()
    {
        Match match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).
            ReturnsAsync(match);

        var command = new CancelMatchByPlayerCommand(Guid.NewGuid(), Guid.NewGuid());
        var handler = new CancelMatchByPlayerCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _reservationReadRepository.Object);
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_fail_when_reservation_is_not_found()
    {
        var reservationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        Match match = Match.Create(reservationId, userId);
        Reservation reservation = null;

        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).
            ReturnsAsync(match);
        _reservationReadRepository.Setup(c => c.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        var command = new CancelMatchByPlayerCommand(userId, match.Id);
        var handler = new CancelMatchByPlayerCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _reservationReadRepository.Object);
        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_fail_when_match_was_previously_cancelled()
    {
        var reservationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        Match match = Match.Create(reservationId, userId);
        match.Cancel();
        Reservation reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(DateTime.Now.AddHours(2).Hour, 30), new TimeOnly(DateTime.Now.AddHours(4).Hour, 0), DateOnly.FromDateTime(DateTime.Now));

        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).
            ReturnsAsync(match);
        _reservationReadRepository.Setup(c => c.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        var command = new CancelMatchByPlayerCommand(userId, match.Id);
        var handler = new CancelMatchByPlayerCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _reservationReadRepository.Object);
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_fail_when_reservation_time_is_smaller_than_one_hour()
    {
        var reservationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        Match match = Match.Create(reservationId, userId);
        Reservation reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(DateTime.Now.Hour, 30), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.Now));

        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).
            ReturnsAsync(match);
        _reservationReadRepository.Setup(c => c.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        var command = new CancelMatchByPlayerCommand(userId, match.Id);
        var handler = new CancelMatchByPlayerCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _reservationReadRepository.Object);
        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_cancel_the_match_succesfully()
    {
        var reservationId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        Match match = Match.Create(reservationId, userId);
        Reservation reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(DateTime.Now.AddHours(4).Hour, 30), new TimeOnly(DateTime.Now.AddHours(6).Hour, 0), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)));

        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>())).
            ReturnsAsync(match);
        _reservationReadRepository.Setup(c => c.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);
        _unitOfWork.Setup(c => c.Repository<Match>().Update(It.IsAny<Match>()));

        var command = new CancelMatchByPlayerCommand(userId, match.Id);
        var handler = new CancelMatchByPlayerCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _reservationReadRepository.Object);

        await handler.Handle(command, default!);

        Assert.True(match.IsCancelled);
        _unitOfWork.Verify(c => c.CompleteAsync(), Times.Once);
    }
}