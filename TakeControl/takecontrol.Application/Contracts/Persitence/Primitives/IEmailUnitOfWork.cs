using takecontrol.Application.Contracts.Persitence.Emails;
using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Contracts.Persitence.Primitives;

public interface IEmailUnitOfWork : IDisposable
{
    IEmailWriteRepository EmailWriteRepository();

    Task<int> CompleteAsync();
}
