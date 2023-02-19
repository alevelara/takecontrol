using Moq;
using takecontrol.Application.Contracts.Persitence.Clubs;
using takecontrol.Application.Exceptions;
using takecontrol.Application.Features.Clubs.Queries.GetByUserId;
using takecontrol.Application.Features.Clubs.Queries.GetClubById;
using takecontrol.Application.Tests.TestsData;
using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Application.UnitTests.Features.Clubs.Queries.GetClubByUserId;

public class GetClubByUserIdQueryHandlerXUnitTests
{
    private readonly Mock<IClubReadRepository> _mockReadRepository;

    public GetClubByUserIdQueryHandlerXUnitTests()
    {
        _mockReadRepository = new();
    }

    [Fact]
    public async Task Handle_Should_TriggerNotFoundException_WhenUserIdDoentExits()
    {
        //Arrange
        var query = new GetClubByUserIdQuery(Guid.NewGuid());
        var handler = new GetClubByUserIdQueryHandler(_mockReadRepository.Object);
        Club club = null;

        //Act

        _mockReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);

        //Assert

        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, default));
    }

    [Fact]
    public async Task Handle_Should_ReturnAClub_WhenUserIdExist()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var query = new GetClubByUserIdQuery(userId);
        var handler = new GetClubByUserIdQueryHandler(_mockReadRepository.Object);
        var address = ApplicationTestData.CreateAddresForTest();
        var club = ApplicationTestData.CreateClubForTest(userId, address);

        //Act
        _mockReadRepository.Setup(c => c.GetClubByUserId(It.IsAny<Guid>()))
            .ReturnsAsync(club);

        var result = await handler.Handle(query, default);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.UserId);
    }
}
