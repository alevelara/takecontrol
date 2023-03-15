using Takecontrol.Domain.Primitives;

namespace Takecontrol.Application.Contracts.Persitence.Primitives;

public interface IUnitOfWork
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
