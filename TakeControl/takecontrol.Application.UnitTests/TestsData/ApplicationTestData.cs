using Takecontrol.Domain.Models.Addresses;
using Takecontrol.Domain.Models.Clubs;
using Takecontrol.Domain.Models.Emails;
using Takecontrol.Domain.Models.PlayerClubs;
using Takecontrol.Domain.Models.Players;
using Takecontrol.Domain.Models.Templates;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.Application.Tests.TestsData;

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
        return Player.Create(userId, "name 1", 1, 1, 1);
    }

    public static Player CreateMidPlayerForTest(Guid userId)
    {
        return Player.Create(userId, "name 2", 1, 3, 2);
    }

    public static Player CreateExpertPlayerForTest(Guid userId)
    {
        return Player.Create(userId, "name 3", 2, 3, 5);
    }

    public static PlayerClub AssignPlayerToClub(Player player, Club club)
    {
        return PlayerClub.Create(player.Id, club.Id);
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
