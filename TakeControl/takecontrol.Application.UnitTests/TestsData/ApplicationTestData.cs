using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Domain.Models.Players;

namespace takecontrol.Application.Tests.TestsData;

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
