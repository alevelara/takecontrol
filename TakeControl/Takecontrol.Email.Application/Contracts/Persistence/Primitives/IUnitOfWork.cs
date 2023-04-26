using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Emails.Application.Contracts.Persistence.Primitives;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
