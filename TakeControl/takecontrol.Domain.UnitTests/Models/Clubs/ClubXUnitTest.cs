﻿using takecontrol.Domain.Models.Clubs;

namespace takecontrol.Domain.UnitTests.Models.Clubs;

public class ClubXUnitTest
{
    [Fact]
    public void Create_Should_ReturnNewClub_WhenAllFieldsArePopulated()
    {
        var addressId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "name";

        var club = Club.Create(addressId, userId, name);

        Assert.NotNull(club);
        Assert.NotNull(club.Id);
        Assert.NotEmpty(club.Code);
        Assert.Equal(club.Name, name);
        Assert.Equal(club.UserId, userId);
        Assert.Equal(club.AddresId, addressId);
    }

    [Fact]
    public void Create_Should_ReturnNewClub_WhenUserIdIsEmpty()
    {
        var addressId = Guid.NewGuid();
        var userId = Guid.Empty;
        var name = "name";

        var club = Club.Create(addressId, userId, name);

        Assert.NotNull(club);
        Assert.NotNull(club.Id);
        Assert.NotEmpty(club.Code);
        Assert.Equal(club.Name, name);
        Assert.Equal(club.UserId, userId);
        Assert.Equal(club.AddresId, addressId);
    }

    [Fact]
    public void Create_Should_ReturnNewClub_WhenAddressIdIsEmpty()
    {
        var addressId = Guid.Empty;
        var userId = Guid.NewGuid();
        var name = "name";

        var club = Club.Create(addressId, userId, name);

        Assert.NotNull(club);
        Assert.NotNull(club.Id);
        Assert.NotEmpty(club.Code);
        Assert.Equal(club.Name, name);
        Assert.Equal(club.UserId, userId);
        Assert.Equal(club.AddresId, addressId);
    }

    [Fact]
    public void Create_Should_ReturnNewClub_WhenNameIsNull()
    {
        var addressId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var club = Club.Create(addressId, userId, null);

        Assert.NotNull(club);
        Assert.NotNull(club.Id);
        Assert.NotEmpty(club.Code);
        Assert.Null(club.Name);
        Assert.Equal(club.UserId, userId);
        Assert.Equal(club.AddresId, addressId);
    }
}
