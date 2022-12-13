using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using takecontrol.Domain.Models.ApplicationUser.Enum;
using takecontrol.Identity.Models;

namespace takecontrol.Identity.Tests.TestsData;

public static class IdentityTestData
{

    public static ApplicationUser CreateApplicationUserForTest()
    {
        return new ApplicationUser
        {
            Email = "user@test.com",
            EmailConfirmed = true,
            Id = Guid.NewGuid(),
            UserName = "Test",
            NormalizedUserName = "TEST",
            UserType = UserType.Administrator,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
    }

    public static IdentityResult CreateFailedIdentityResult()
    {
        return IdentityResult.Failed(new IdentityError
        {
            Code = "",
            Description = "Error"
        });
    }

    public static IdentityResult CreateSuccededIdentityResult()
    {
        return IdentityResult.Success;
    }
}
