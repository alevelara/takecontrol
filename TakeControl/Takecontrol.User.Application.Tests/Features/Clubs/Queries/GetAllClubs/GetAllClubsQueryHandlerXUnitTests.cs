﻿using Moq;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Features.Clubs.Queries.GetAllClubs;
using Takecontrol.User.Domain.Models.Clubs;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Clubs.Queries.GetAllClubs;

[Trait("Category", Category.UnitTest)]
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
            .ReturnsAsync(new List<Club>() { Club.Create(Guid.NewGuid(), Guid.NewGuid(), "name", 1, new TimeOnly(10, 0), new TimeOnly(12, 0)) });

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
