using Moq;
using takecontrol.Application.Contracts.Persitence.Players;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Players.Queries.GetAllPlayersByClubId;
using takecontrol.Application.Tests.TestsData;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.UnitTests.Features.Players.Queries.GetAllPlayersByClubId;

public class GetAllPlayersByClubIdHandlerXUnitTests
{
    private readonly Mock<IPlayerReadRepository> _mockReadRepository;

    public GetAllPlayersByClubIdHandlerXUnitTests()
    {
        _mockReadRepository = new();
    }

    [Fact]
    public async Task Handle_Should_TriggerNotFoundException_WhenUserIdDoentExits()
    {
        //Arrange
        var query = new GetAllPlayersByClubIdQuery(Guid.NewGuid());
        var handler = new GetAllPlayersByClubIdQueryHandler(_mockReadRepository.Object);
        Player player = null;

        //Act
        _mockReadRepository.Setup(c => c.GetAllPlayersByClubId(It.IsAny<Guid>()));

        //Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, default));
    }

    [Fact]
    public async Task Handle_Should_PlayersThatBelongToAClub()
    {
        //Arrange
        var userIdClub = Guid.NewGuid();
        var userId1ClubA = Guid.NewGuid();
        var userId2ClubA = Guid.NewGuid();
        var userIdClubB = Guid.NewGuid();

        // var queryBegginer = new GetPlayerByIdQuery(userIdBegginer);
        // var queryMid = new GetPlayerByIdQuery(userIdMid);
        // var queryExpert = new GetPlayerByIdQuery(userIdExpert);

        var handler = new GetAllPlayersByClubIdQueryHandler(_mockReadRepository.Object);
        var player1ClubA = ApplicationTestData.CreateBegginerPlayerForTest(userId1ClubA);
        var player2ClubA = ApplicationTestData.CreateBegginerPlayerForTest(userId2ClubA);
        var playerClubB = ApplicationTestData.CreateMidPlayerForTest(userIdClubB);
        

        // Create a Club
        var addressA = ApplicationTestData.CreateAddresForTest();
        var addressB = ApplicationTestData.CreateAddresForTest();
        var clubA = ApplicationTestData.CreateClubForTest(userIdClub, addressA);
        var clubB = ApplicationTestData.CreateClubForTest(userIdClub, addressB);

        // Add Users to Club
        

        //Acts
        _mockReadRepository.Setup(c => c.GetAllPlayersByClubId(It.IsAny<Guid>()))
            .ReturnsAsync(playerBeginner);
        _mockReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(playerMid);
        _mockReadRepository.Setup(c => c.GetPlayerByUserId(It.IsAny<Guid>()))
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
