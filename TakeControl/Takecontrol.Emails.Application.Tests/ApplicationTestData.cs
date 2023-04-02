using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Domain.Models.Templates.Enum;

namespace Takecontrol.Emails.Application.Tests;

public static class ApplicationTestData
{
    public static Email CreateEmailForTest()
    {
        return Email.Create("email@test.com", "subjectTest", TemplateType.WELCOME);
    }

    public static Template CreateTemplateForTest()
    {
        return Template.Create(TemplateType.WELCOME, "payload", "es");
    }
}
