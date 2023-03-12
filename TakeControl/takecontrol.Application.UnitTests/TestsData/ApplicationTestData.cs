using takecontrol.Domain.Models.Addresses;
using takecontrol.Domain.Models.Clubs;
using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Players;
using takecontrol.Domain.Models.Templates;
using takecontrol.Domain.Models.Templates.Enum;

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

    public static Email CreateEmailForTest()
    {
        return Email.Create("email@test.com", "subjectTest", TemplateType.WELCOME);
    }

    public static Template CreateTemplateForTest()
    {
        return Template.Create(TemplateType.WELCOME, "payload", "es");
    }
}
