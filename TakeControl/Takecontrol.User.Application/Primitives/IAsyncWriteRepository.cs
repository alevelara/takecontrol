using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.User.Application.Primitives;

public interface IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    Task<T> AddAsync(T entity);

    T UpdateAsync(T entity);

    void DeleteAsync(T entity);
}
