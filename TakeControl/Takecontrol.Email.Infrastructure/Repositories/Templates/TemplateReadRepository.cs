using Microsoft.EntityFrameworkCore;
using Takecontrol.Emails.Application.Contracts.Persitence.Templates;
using Takecontrol.Emails.Domain.Models.Templates;
using Takecontrol.Emails.Domain.Models.Templates.Enum;
using Takecontrol.Emails.Infrastructure.Contexts;

namespace Takecontrol.Emails.Infrastructure.Repositories.Templates;

public class TemplateReadRepository : ITemplateAsyncReadRepository
{
    private readonly EmailDbContext _emailDbContext;

    public TemplateReadRepository(EmailDbContext emailDbContext)
    {
        _emailDbContext = emailDbContext;
    }

    public async Task<Template?> GetTemplateByTemplateType(TemplateType templateType)
    {
        return await _emailDbContext!.Templates!.FirstOrDefaultAsync(c => c.TemplateType == templateType)!;
    }
}
