using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Application.Primitives;

public interface IUnitOfWork : IDisposable
{
    IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel;

    Task<int> CompleteAsync();
}
