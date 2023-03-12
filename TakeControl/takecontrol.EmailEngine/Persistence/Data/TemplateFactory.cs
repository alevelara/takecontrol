using takecontrol.Domain.Models.Templates;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.EmailEngine.Persistence.Data;

public static class TemplateFactory
{
    public static Template WelcomeTemplate = Template.Create(TemplateType.WELCOME, TemplateData.WelcomeTemplate(), "ES");
}
