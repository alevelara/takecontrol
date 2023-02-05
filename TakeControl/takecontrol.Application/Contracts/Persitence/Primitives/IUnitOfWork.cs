using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Contracts.Persitence.Primitives;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
