using System.Linq.Expressions;
using takecontrol.Domain.Primitives;

namespace takecontrol.Application.Contracts.Persitence;

public interface IAsyncReadRepository<T> where T : BaseDomainModel
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    List<Expression<Func<T, object>>> includes = null,
                                    string includeString = null,
                                    bool disableTracking = true);    
}
