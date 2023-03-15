using Takecontrol.Application.Contracts.Persitence.Emails;
using Takecontrol.Domain.Models.Emails;
using Takecontrol.EmailEngine.Persistence.Contexts;
using Takecontrol.EmailEngine.Repositories.Primitives;

namespace Takecontrol.EmailEngine.Repositories.Emails;

public class EmailWriteRepository : WriteBaseRepository<Email>, IEmailWriteRepository
{
    private readonly EmailDbContext _emailDbContext;

    public EmailWriteRepository(EmailDbContext context) : base(context)
    {
        _emailDbContext = context;
    }

    public async Task<Email> AddEmail(Email email)
    {
        _emailDbContext.Set<Email>().AddAsync(email);
        return email;
    }
}
