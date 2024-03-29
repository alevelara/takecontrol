using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Players;
using Takecontrol.User.Application.Features.Players.Queries.GetPlayer;
using Takecontrol.User.Domain.Models.Players;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Players.Queries.GetPlayerById;

[Trait("Category", Category.UnitTest)]
public class GetPlayerByIdQueryHandlerXUnitTests
{
    private readonly Mock<IPlayerReadRepository> _mockReadRepository;

    public GetPlayerByIdQueryHandlerXUnitTests()
    {
        _mockReadRepository = new();
    }

    [Fact]
    public async Task Handle_Should_TriggerNotFoundException_WhenUserIdDoentExits()
    {
        //Arrange
        var query = new GetPlayerByIdQuery(Guid.NewGuid());
        var handler = new GetPlayerByIdQueryHandler(_mockReadRepository.Object);
        Player player = null;

        //Act
        _mockReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(player);

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, default));
    }

    [Fact]
    public async Task Handle_Should_ReturnAClub_WhenUserIdExist()
    {
        //Arrange
        var userIdBegginer = Guid.NewGuid();
        var userIdMid = Guid.NewGuid();
        var userIdExpert = Guid.NewGuid();

        var queryBegginer = new GetPlayerByIdQuery(userIdBegginer);
        var queryMid = new GetPlayerByIdQuery(userIdMid);
        var queryExpert = new GetPlayerByIdQuery(userIdExpert);

        var handler = new GetPlayerByIdQueryHandler(_mockReadRepository.Object);
        var playerBeginner = ApplicationTestData.CreateBegginerPlayerForTest(userIdBegginer);
        var playerMid = ApplicationTestData.CreateMidPlayerForTest(userIdMid);
        var playerExpert = ApplicationTestData.CreateExpertPlayerForTest(userIdExpert);

        //Acts
        _mockReadRepository.Setup(c => c.GetPlayerByUserId(userIdBegginer))
            .ReturnsAsync(playerBeginner);
        _mockReadRepository.Setup(c => c.GetPlayerByUserId(userIdMid))
            .ReturnsAsync(playerMid);
        _mockReadRepository.Setup(c => c.GetPlayerByUserId(userIdExpert))
            .ReturnsAsync(playerExpert);

        var resultBeginner = await handler.Handle(queryBegginer, default);
        var resultMid = await handler.Handle(queryMid, default);
        var resultExpert = await handler.Handle(queryExpert, default);

        //Asserts
        Assert.NotNull(resultBeginner);
        Assert.Equal(userIdBegginer, resultBeginner.UserId);
        Assert.NotNull(resultMid);
        Assert.Equal(userIdMid, resultMid.UserId);
        Assert.NotNull(resultExpert);
        Assert.Equal(userIdExpert, resultExpert.UserId);
    }
}
