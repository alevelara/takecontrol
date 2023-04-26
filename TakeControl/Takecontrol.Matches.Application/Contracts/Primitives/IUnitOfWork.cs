using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Application.Contracts.Primitives;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
