using MediatR;
using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Application.Messages.Matches;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Features.Clubs.Commands.CancelForcedMatch;
using Takecontrol.User.Domain.Models.Clubs;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Clubs.Commands.CancelForcedMatch;

[Trait("Category", Category.UnitTest)]
public class CancelForcedMatchCommandHandlerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IClubReadRepository> _clubReadRepository;

    public CancelForcedMatchCommandHandlerTests()
    {
        _mediator = new();
        _clubReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_club_does_not_exist()
    {
        Club club = null;
        _clubReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);

        var command = new CancelForcedMatchCommand(Guid.NewGuid(), Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchCommandHandler(_mediator.Object, _clubReadRepository.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_continue_with_cancellation_when_club_exists()
    {
        var address = ApplicationTestData.CreateAddresForTest();
        var club = ApplicationTestData.CreateClubForTest(Guid.NewGuid(), address);
        _clubReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);

        var command = new CancelForcedMatchCommand(Guid.NewGuid(), Guid.NewGuid(), "description");
        var handler = new CancelForcedMatchCommandHandler(_mediator.Object, _clubReadRepository.Object);
        var result = await handler.Handle(command, default);

        Assert.IsType<Unit>(result);
        _mediator.Verify(c => c.Send(It.IsAny<CancelForcedMatchByClubCommand>(), default!), Times.Once);
    }
}
