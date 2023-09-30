using MediatR;
using Moq;
using Takecontrol.Emails.Application.Contracts.Persistence.Primitives;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Commands.CancelMatch;

[Trait("Category", Category.UnitTest)]
public class CancelMatchCommandHandlerTests
{
    private readonly Mock<IMediator> _mediator;
    private readonly Mock<IAsyncReadRepository<Player>> _playerReadRepository;

    public CancelMatchCommandHandlerTests()
    {
        _mediator = new();
        _playerReadRepository = new();
    }

    [Fact]
    public async Task Should_fail_when_user_does_not_exist()
    {

    }

    [Fact]
    public async Task Should_cancel_the_match_successfully()
    {
    
    }
}
