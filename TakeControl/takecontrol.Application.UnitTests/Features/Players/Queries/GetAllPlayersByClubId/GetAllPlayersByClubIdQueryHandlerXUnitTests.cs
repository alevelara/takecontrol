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
        var userClubA = Guid.NewGuid();
        var userClubB = Guid.NewGuid();
        var userIdPlayer1 = Guid.NewGuid();
        var userIdPlayer2 = Guid.NewGuid();
        var userIdPlayer3 = Guid.NewGuid();

        var handler = new GetAllPlayersByClubIdQueryHandler(_mockReadRepository.Object);
        var player1ClubA = ApplicationTestData.CreateBegginerPlayerForTest(userIdPlayer1);
        var player2ClubA = ApplicationTestData.CreateExpertPlayerForTest(userIdPlayer2);
        var player3ClubB = ApplicationTestData.CreateMidPlayerForTest(userIdPlayer3);
        

        // Create a Club
        var addressA = ApplicationTestData.CreateAddresForTest();
        var addressB = ApplicationTestData.CreateAddresForTest();
        var clubA = ApplicationTestData.CreateClubForTest(userClubA, addressA);
        var clubB = ApplicationTestData.CreateClubForTest(userClubB, addressB);

        // Add Users to Club
        var player1AssignedClubA = ApplicationTestData.AssignPlayerToClub(player1ClubA, clubA);
        var player2AssignedClubA = ApplicationTestData.AssignPlayerToClub(player2ClubA, clubA);
        var player3AssignedClubB = ApplicationTestData.AssignPlayerToClub(player3ClubB, clubB);
        
        //Acts
        _mockReadRepository.Setup(c => c.GetAllPlayersByClubId(clubA.Id))
            .ReturnsAsync(new List<Player>() { player1ClubA, player2ClubA });
        _mockReadRepository.Setup(c => c.GetAllPlayersByClubId(clubB.Id))
            .ReturnsAsync(new List<Player>() { player3ClubB });

         // Queries
        var queryClubA = new GetAllPlayersByClubIdQuery(clubA.Id);
        var queryClubB = new GetAllPlayersByClubIdQuery(clubB.Id);

        var resultClubA = await handler.Handle(queryClubA, default);
        var resultClubB = await handler.Handle(queryClubB, default);
        
        Assert.NotNull(resultClubA);
        Assert.Equal(2, resultClubA.Count);
        Assert.NotNull(resultClubB);
        Assert.Equal(1, resultClubB.Count);
    }
}
