using Takecontrol.Domain.Models.Templates;
using Takecontrol.Domain.Models.Templates.Enum;

namespace Takecontrol.Application.Contracts.Persitence.Templates;

public interface ITemplateAsyncReadRepository
{
    Task<Template> GetTemplateByTemplateType(TemplateType templateType);
}
