using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Application.Contracts.Primitives;

public interface IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    Task<T> AddAsync(T entity);

    Task AddRangeAsync(List<T> entities);

    T Update(T entity);

    void Delete(T entity);
}
