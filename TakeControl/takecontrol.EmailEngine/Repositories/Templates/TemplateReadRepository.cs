using Microsoft.EntityFrameworkCore;
using Takecontrol.Application.Contracts.Persitence.Templates;
using Takecontrol.Domain.Models.Templates;
using Takecontrol.Domain.Models.Templates.Enum;
using Takecontrol.EmailEngine.Persistence.Contexts;

namespace Takecontrol.EmailEngine.Repositories.Templates;

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
