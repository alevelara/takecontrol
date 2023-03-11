using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Shared.Application.Contracts.Persitence.Primitives;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
