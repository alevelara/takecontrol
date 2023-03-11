using Takecontrol.Emails.Domain.Models.Emails;

namespace Takecontrol.Emails.Application.Contracts.Persitence.Emails
{
    public interface IEmailWriteRepository
    {
        Task<Email> AddEmail(Email email);
    }
}
