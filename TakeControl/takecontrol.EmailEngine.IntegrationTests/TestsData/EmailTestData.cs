using Takecontrol.Domain.Models.Emails;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.EmailEngine.UnitTests.TestsData;

public static class EmailTestData
{
    public static Email CreateEmailForTest()
    {
        return Email.Create("email@test.com", "subject", TemplateType.WELCOME);
    }
}
