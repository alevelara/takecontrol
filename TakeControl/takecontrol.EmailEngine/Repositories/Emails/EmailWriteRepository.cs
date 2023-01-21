using takecontrol.Application.Contracts.Persitence.Emails;
using takecontrol.Domain.Models.Emails;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.EmailEngine.Repositories.Primitives;

namespace takecontrol.EmailEngine.Repositories.Emails;

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
