using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;

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
}
