using Moq;
using Takecontrol.Application.Contracts.Persitence.Clubs;
using Takecontrol.Application.Features.Clubs.Queries.GetAllClubs;
using Takecontrol.Domain.Models.Clubs;

namespace Takecontrol.Application.UnitTests.Features.Clubs.Queries.GetAllClubs;

public class GetAllClubsQueryHandlerXunitTests
{
    private readonly Mock<IClubReadRepository> _clubReadRepository;

    public GetAllClubsQueryHandlerXunitTests()
    {
        _clubReadRepository = new();
    }

    [Fact]
    public async Task Handle_Should_ReturnAllClubs_WhenClubsExistInDatabase()
    {
        //Arrange
        var query = new GetAllClubsQuery();
        var handler = new GetAllClubsQueryHandler(_clubReadRepository.Object);

        _clubReadRepository.Setup(c => c.GetAllClubsAsync())
            .ReturnsAsync(new List<Club>() { Club.Create(Guid.NewGuid(), Guid.NewGuid(), "name") });

        //Act
        var result = await handler.Handle(query, default);

        //Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenClubsDontExistInDatabase()
    {
        //Arrange
        var query = new GetAllClubsQuery();
        var handler = new GetAllClubsQueryHandler(_clubReadRepository.Object);

        _clubReadRepository.Setup(c => c.GetAllClubsAsync())
            .ReturnsAsync(new List<Club>());

        //Act
        var result = await handler.Handle(query, default);

        //Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
