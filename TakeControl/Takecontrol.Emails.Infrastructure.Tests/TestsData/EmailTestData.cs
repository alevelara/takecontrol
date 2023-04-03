using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Templates.Enum;

namespace Takecontrol.Emails.Infrastructure.Tests.TestsData;

public static class EmailTestData
{
    public static Email CreateEmailForTest()
    {
        return Email.Create("email@test.com", "subject", TemplateType.WELCOME);
    }
}
