using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Domain.Models.Templates.Enum;

namespace Takecontrol.Emails.Application.Contracts.Persitence.Templates;

public interface ITemplateAsyncReadRepository
{
    Task<Template?> GetTemplateByTemplateType(TemplateType templateType);
}
