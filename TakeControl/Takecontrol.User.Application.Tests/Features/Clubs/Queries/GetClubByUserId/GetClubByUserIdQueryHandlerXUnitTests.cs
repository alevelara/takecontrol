﻿using Moq;
using Takecontrol.Shared.Application.Exceptions;
using Takecontrol.Shared.Tests.Constants;
using Takecontrol.User.Application.Contracts.Persistence.Clubs;
using Takecontrol.User.Application.Features.Clubs.Queries.GetClubByUserId;
using Takecontrol.User.Application.Tests;
using Takecontrol.User.Domain.Models.Clubs;
using Xunit;

namespace Takecontrol.User.Application.Tests.Features.Clubs.Queries.GetClubByUserId;

[Trait("Category", Category.UnitTest)]
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
