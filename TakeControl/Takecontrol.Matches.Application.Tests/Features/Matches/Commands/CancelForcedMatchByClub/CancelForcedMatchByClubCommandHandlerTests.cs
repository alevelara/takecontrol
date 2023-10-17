using MediatR;
using Moq;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;
using Takecontrol.Matches.Application.Contracts.Persistence.Reservations;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Matches.Commands.CancelForcedMatchByClub;
using Takecontrol.Matches.Domain.Models.Courts;
using Takecontrol.Matches.Domain.Models.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.Reservations;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;

namespace Takecontrol.Matches.Application.Tests.Features.Matches.Commands.CancelForcedMatchByClub;

[Trait("Category", Category.UnitTest)]
public class CancelForcedMatchByClubCommandHandlerTests
{
    private readonly Mock<IMatchReadRepository> _matchReadRepository;
    private readonly Mock<IMatchPlayerReadRepository> _matchPlayerReadRepository;
    private readonly Mock<IReservationReadRepository> _reservationReadRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;

    public CancelForcedMatchByClubCommandHandlerTests()
    {
        _matchReadRepository = new();
        _matchPlayerReadRepository = new();
        _reservationReadRepository = new();
        _unitOfWork = new();
    }

    [Fact]
    public async Task Should_fail_when_match_does_not_exist()
    {
        Match match = null;
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);

        var command = new CancelForcedMatchByClubCommand(Guid.NewGuid(), Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchByClubCommandHandler(_matchReadRepository.Object, _reservationReadRepository.Object, _matchPlayerReadRepository.Object, _unitOfWork.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_fail_when_reservation_does_not_exist()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        Reservation reservation = null;
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);
        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        var command = new CancelForcedMatchByClubCommand(Guid.NewGuid(), Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchByClubCommandHandler(_matchReadRepository.Object, _reservationReadRepository.Object, _matchPlayerReadRepository.Object, _unitOfWork.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_fail_when_court_does_not_exist()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        Reservation reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(10, 30), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.UtcNow));
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);
        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        var command = new CancelForcedMatchByClubCommand(Guid.NewGuid(), Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchByClubCommandHandler(_matchReadRepository.Object, _reservationReadRepository.Object, _matchPlayerReadRepository.Object, _unitOfWork.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_fail_when_other_club_tries_to_cancel_the_match()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        Reservation reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(10, 30), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.UtcNow));
        reservation.SetCourt(Court.Create(Guid.NewGuid(), "name"));
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);
        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);

        var command = new CancelForcedMatchByClubCommand(Guid.NewGuid(), Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchByClubCommandHandler(_matchReadRepository.Object, _reservationReadRepository.Object, _matchPlayerReadRepository.Object, _unitOfWork.Object);

        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_cancel_match_succesfully()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        var clubId = Guid.NewGuid();
        Reservation reservation = Reservation.Create(Guid.NewGuid(), new TimeOnly(10, 30), new TimeOnly(12, 0), DateOnly.FromDateTime(DateTime.UtcNow));
        reservation.SetCourt(Court.Create(clubId, "name"));
        _matchReadRepository.Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);
        _reservationReadRepository.Setup(r => r.GetReservationById(It.IsAny<Guid>()))
            .ReturnsAsync(reservation);
        _matchPlayerReadRepository.Setup(c => c.GetMatchPlayersByMatchId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<MatchPlayer>());
        _unitOfWork.Setup(c => c.Repository<Match>().Update(It.IsAny<Match>()));

        var command = new CancelForcedMatchByClubCommand(clubId, Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchByClubCommandHandler(_matchReadRepository.Object, _reservationReadRepository.Object, _matchPlayerReadRepository.Object, _unitOfWork.Object);
        var result = await handler.Handle(command, default);

        Assert.IsType<Unit>(result);
        _unitOfWork.Verify(c => c.CompleteAsync(), Times.Once());
    }
}
