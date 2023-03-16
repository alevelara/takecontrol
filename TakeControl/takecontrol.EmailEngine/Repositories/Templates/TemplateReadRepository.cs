using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence.Templates;
using takecontrol.Domain.Models.Templates;
using takecontrol.Domain.Models.Templates.Enum;
using takecontrol.EmailEngine.Persistence.Contexts;

namespace takecontrol.EmailEngine.Repositories.Templates;

public class TemplateReadRepository : ITemplateAsyncReadRepository
{
    private readonly EmailDbContext _emailDbContext;

    public TemplateReadRepository(EmailDbContext emailDbContext)
    {
        _emailDbContext = emailDbContext;
    }

    public async Task<Template> GetTemplateByTemplateType(TemplateType templateType)
    {
        return await _emailDbContext.Templates?.FirstOrDefaultAsync(c => c.TemplateType == templateType);
    }
}
