using takecontrol.Domain.Models.Emails;

namespace takecontrol.Application.Contracts.Persitence.Emails
{
    public interface IEmailWriteRepository
    {
        Task<Email> AddEmail(Email email);
    }
}
