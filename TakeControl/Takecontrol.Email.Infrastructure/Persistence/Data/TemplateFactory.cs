using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Domain.Models.Templates.Enum;

namespace Takecontrol.Emails.Infrastructure.Persistence.Data;

public static class TemplateFactory
{
    public static Template WelcomeTemplate = Template.Create(TemplateType.WELCOME, TemplateData.WelcomeTemplate(), "ES");
    public static Template CancelByPlayerTemplate = Template.Create(TemplateType.CANCELLED_FOR_PLAYER, TemplateData.CancelByPlayerTemplate(), "ES");
}
