using takecontrol.Domain.Models.Emails;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.EmailEngine.UnitTests.TestsData;

public static class EmailTestData
{
    public static Email CreateEmailForTest()
    {
        return Email.Create("email@test.com", "subject", TemplateType.WELCOME);
    }
}
