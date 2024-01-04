using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Application.Primitives;

public interface IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    Task<T> AddAsync(T entity);

    T Update(T entity);

    void Delete(T entity);
}
