using MediatR;
using Moq;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Matches.Commands.UnsubscribePlayerFromMatch;
using Takecontrol.Matches.Application.Tests.TestData.Matches;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;

namespace Takecontrol.Matches.Application.Tests.Features.Matches.Commands.UnsubscribePlayerFromMatch;

[Trait("Category", Category.UnitTest)]
public class UnsubscribePlayerFromMatchCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMatchReadRepository> _matchReadRepository;
    private readonly Mock<IMatchPlayerReadRepository> _playerMatchReadRepository;

    public UnsubscribePlayerFromMatchCommandHandlerTests()
    {
        _unitOfWork = new();
        _matchReadRepository = new();
        _playerMatchReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_if_match_was_not_created_previously()
    {
        var request = new UnsubscribePlayerFromMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        Match? match = null;
        var handler = new UnsubscribePlayerFromMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _playerMatchReadRepository.Object);
        _matchReadRepository.Setup(m => m.GetMatchWithReservation(request.MatchId))
            .ReturnsAsync(match);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
        _matchReadRepository.Verify(m => m.GetMatchWithReservation(request.MatchId), Times.Once());
    }

    [Fact]
    public async Task Should_fail_when_player_tries_to_unbsubscribe_from_the_match_within_one_day()
    {
        var request = new UnsubscribePlayerFromMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var match = MatchTestData.CreateMatchWithReservationForTest(DateTime.UtcNow, request.PlayerId);
        var handler = new UnsubscribePlayerFromMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _playerMatchReadRepository.Object);
        _matchReadRepository.Setup(m => m.GetMatchWithReservation(request.MatchId))
            .ReturnsAsync(match);

        await Assert.ThrowsAsync<ConflictException>(() => handler.Handle(request, default));
        _matchReadRepository.Verify(m => m.GetMatchWithReservation(request.MatchId), Times.Once());
    }

    [Fact]
    public async Task Should_fail_when_player_was_not_subscribe_to_the_match_previously()
    {
        var request = new UnsubscribePlayerFromMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var match = MatchTestData.CreateMatchWithReservationForTest(DateTime.UtcNow.AddDays(-2), request.PlayerId);
        var handler = new UnsubscribePlayerFromMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _playerMatchReadRepository.Object);
        _matchReadRepository.Setup(m => m.GetMatchWithReservation(request.MatchId))
            .ReturnsAsync(match);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
        _matchReadRepository.Verify(m => m.GetMatchWithReservation(request.MatchId), Times.Once());
    }

    [Fact]
    public async Task Should_be_unbsubscribed_from_the_match_and_match_must_be_available_again()
    {
        var request = new UnsubscribePlayerFromMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        var match = MatchTestData.CreateMatchWithReservationForTest(DateTime.UtcNow.AddDays(-1).AddMinutes(-1), request.PlayerId);
        var matchPlayer = MatchTestData.CreateMatchPlayerForTest(match.Id, request.PlayerId);
        var handler = new UnsubscribePlayerFromMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _playerMatchReadRepository.Object);
        _matchReadRepository.Setup(m => m.GetMatchWithReservation(It.IsAny<Guid>()))
            .ReturnsAsync(match);
        _playerMatchReadRepository.Setup(m => m.GetMatchPlayerByPlayerIdAndMatchId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(matchPlayer);
        _unitOfWork.Setup(u => u.Repository<MatchPlayer>().Delete(It.IsAny<MatchPlayer>()));
        _unitOfWork.Setup(u => u.Repository<Match>().Update(It.IsAny<Match>()));

        var result = await handler.Handle(request, default);

        Assert.Equal(Unit.Value, result);
        _matchReadRepository.Verify(m => m.GetMatchWithReservation(request.MatchId), Times.Once());
        _playerMatchReadRepository.Verify(m => m.GetMatchPlayerByPlayerIdAndMatchId(request.PlayerId, request.MatchId), Times.Once());
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once());
    }
}
