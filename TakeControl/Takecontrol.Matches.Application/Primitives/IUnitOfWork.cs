using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Application.Primitives;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
