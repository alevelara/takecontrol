using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Contracts.Persitence;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
