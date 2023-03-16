using takecontrol.Domain.Models.Templates;
using takecontrol.Domain.Models.Templates.Enum;

namespace takecontrol.Application.Contracts.Persitence.Templates;

public interface ITemplateAsyncReadRepository
{
    Task<Template> GetTemplateByTemplateType(TemplateType templateType);
}
