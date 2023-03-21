﻿using Takecontrol.User.Domain.Models.Addresses;
using Takecontrol.User.Domain.Models.Clubs;
using Takecontrol.User.Domain.Models.Players;

namespace Takecontrol.User.Application.Tests;

public static class ApplicationTestData
{
    public static Address CreateAddresForTest()
    {
        return Address.Create("cityTest", "provinceTest", "mainAddressTest");
    }

    public static Club CreateClubForTest(Guid userId, Address address)
    {
        return Club.Create(address.Id, userId, "nameTest");
    }

    public static Player CreateBegginerPlayerForTest(Guid userId)
    {
        return Player.Create(userId, "name", 1, 1, 1);
    }

    public static Player CreateMidPlayerForTest(Guid userId)
    {
        return Player.Create(userId, "name", 1, 3, 2);
    }

    public static Player CreateExpertPlayerForTest(Guid userId)
    {
        return Player.Create(userId, "name", 2, 3, 5);
    }
}