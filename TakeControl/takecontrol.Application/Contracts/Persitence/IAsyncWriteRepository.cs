using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Contracts.Persitence;

public interface IAsyncWriteRepository<T> where T : BaseDomainModel
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
