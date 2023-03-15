using Takecontrol.Domain.Models.Templates;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.EmailEngine.Persistence.Data;

public static class TemplateFactory
{
    public static Template WelcomeTemplate = Template.Create(TemplateType.WELCOME, TemplateData.WelcomeTemplate(), "ES");
}
