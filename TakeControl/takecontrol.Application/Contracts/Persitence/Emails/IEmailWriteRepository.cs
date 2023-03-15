using Takecontrol.Domain.Models.Emails;

namespace Takecontrol.Application.Contracts.Persitence.Emails
{
    public interface IEmailWriteRepository
    {
        Task<Email> AddEmail(Email email);
    }
}
