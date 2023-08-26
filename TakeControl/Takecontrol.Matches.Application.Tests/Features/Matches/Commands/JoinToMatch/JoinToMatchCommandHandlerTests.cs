using MediatR;
using Moq;
using Takecontrol.Matches.Application.Contracts.Persistence.Matches;
using Takecontrol.Matches.Application.Contracts.Persistence.MatchPlayers;
using Takecontrol.Matches.Application.Contracts.Primitives;
using Takecontrol.Matches.Application.Features.Matches.Commands.JoinToMatch;
using Takecontrol.Matches.Domain.Models.MatchPlayers;
using Takecontrol.Matches.Domain.Models.MatchPlayers.ValueObjects;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Match = Takecontrol.Matches.Domain.Models.Matches.Match;

namespace Takecontrol.Matches.Application.Tests.Features.Matches.Commands.JoinToMatch;

[Trait("Category", Category.UnitTest)]
public class JoinToMatchCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMatchReadRepository> _matchReadRepository;
    private readonly Mock<IMatchPlayerReadRepository> _matchPlayerReadRepository;

    public JoinToMatchCommandHandlerTests()
    {
        _unitOfWork = new();
        _matchReadRepository = new();
        _matchPlayerReadRepository = new();
    }

    [Fact]
    public async Task Should_failed_when_player_is_trying_to_register_to_an_existing_match()
    {
        Match? match = null;
        _matchReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);

        var handler = new JoinToMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _matchPlayerReadRepository.Object);
        var command = new JoinToMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_failed_when_player_tries_to_register_in_a_closed_match()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        match.Close();
        _matchReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);

        var handler = new JoinToMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _matchPlayerReadRepository.Object);
        var command = new JoinToMatchCommand(Guid.NewGuid(), Guid.NewGuid());

        await Assert.ThrowsAsync<ConflictException>(async () => await handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_failed_when_player_was_already_registered_in_a_match()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        var matchPlayer = MatchPlayer.Create(Guid.NewGuid(), Guid.NewGuid());
        _matchReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(match);
        _matchPlayerReadRepository.Setup(x => x.GetMatchPlayerByPlayerIdAndMatchId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(matchPlayer);

        var handler = new JoinToMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _matchPlayerReadRepository.Object);
        var command = new JoinToMatchCommand(Guid.NewGuid(), Guid.NewGuid());
        await Assert.ThrowsAsync<ConflictException>(async () => await handler.Handle(command, default!));
    }

    [Fact]
    public async Task Should_register_the_player_and_close_the_match_when_player_is_the_last_one_to_join()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        MatchPlayer? matchPlayer = null;
        var matchPlayerForList = MatchPlayer.Create(Guid.NewGuid(), Guid.NewGuid());
        var matchWriteRepository = new Mock<IAsyncWriteRepository<Match>>();
        var matchPlayerWriteRepository = new Mock<IAsyncWriteRepository<MatchPlayer>>();

        _matchReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(match);
        _matchPlayerReadRepository.Setup(x => x.GetMatchPlayerByPlayerIdAndMatchId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(matchPlayer);
        _matchPlayerReadRepository.Setup(x => x.GetMatchPlayersByMatchId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<MatchPlayer> { matchPlayerForList, matchPlayerForList, matchPlayerForList });
        _unitOfWork.Setup(c => c.Repository<Match>()).Returns(matchWriteRepository.Object);
        _unitOfWork.Setup(c => c.Repository<MatchPlayer>()).Returns(matchPlayerWriteRepository.Object);

        var handler = new JoinToMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _matchPlayerReadRepository.Object);
        var command = new JoinToMatchCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await handler.Handle(command, default);

        Assert.Equal(result, Unit.Value);
        _unitOfWork.Verify(x => x.Repository<Match>().Update(It.IsAny<Match>()), Times.Once);
        _unitOfWork.Verify(x => x.Repository<MatchPlayer>().AddAsync(It.IsAny<MatchPlayer>()), Times.Once);
        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task Should_register_the_player_and_match_will_remains_opened_when_player_is_not_the_last_one()
    {
        var match = Match.Create(Guid.NewGuid(), Guid.NewGuid());
        MatchPlayer? matchPlayer = null;
        var matchPlayerForList = MatchPlayer.Create(Guid.NewGuid(), Guid.NewGuid());
        var matchWriteRepository = new Mock<IAsyncWriteRepository<Match>>();
        var matchPlayerWriteRepository = new Mock<IAsyncWriteRepository<MatchPlayer>>();

        _matchReadRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
           .ReturnsAsync(match);
        _matchPlayerReadRepository.Setup(x => x.GetMatchPlayerByPlayerIdAndMatchId(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(matchPlayer);
        _matchPlayerReadRepository.Setup(x => x.GetMatchPlayersByMatchId(It.IsAny<Guid>()))
            .ReturnsAsync(new List<MatchPlayer> { matchPlayerForList, matchPlayerForList });
        _unitOfWork.Setup(c => c.Repository<Match>()).Returns(matchWriteRepository.Object);
        _unitOfWork.Setup(c => c.Repository<MatchPlayer>()).Returns(matchPlayerWriteRepository.Object);

        var handler = new JoinToMatchCommandHandler(_unitOfWork.Object, _matchReadRepository.Object, _matchPlayerReadRepository.Object);
        var command = new JoinToMatchCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await handler.Handle(command, default);

        Assert.Equal(result, Unit.Value);
        _unitOfWork.Verify(x => x.Repository<MatchPlayer>().AddAsync(It.IsAny<MatchPlayer>()), Times.Once);
        _unitOfWork.Verify(x => x.Repository<MatchPlayer>().Update(It.IsAny<MatchPlayer>()), Times.Never);
        _unitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
    }
}
