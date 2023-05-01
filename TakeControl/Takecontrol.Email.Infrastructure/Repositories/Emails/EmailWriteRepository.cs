using Takecontrol.Emails.Application.Contracts.Persitence.Emails;
using Takecontrol.Emails.Domain.Models.Emails;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Emails.Infrastructure.Repositories.Primitives;

namespace Takecontrol.Emails.Infrastructure.Repositories.Emails;

public class EmailWriteRepository : WriteBaseRepository<Email>, IEmailWriteRepository
{
    private readonly EmailDbContext _emailDbContext;

    public EmailWriteRepository(EmailDbContext context) : base(context)
    {
        _emailDbContext = context;
    }

    public async Task<Email> AddEmail(Email email)
    {
        await _emailDbContext.Set<Email>().AddAsync(email);
        return email;
    }
}
