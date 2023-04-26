using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Application.Primitives;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Infrastructure.Repositories.Primitives;

public class ReadBaseRepository<T> : IAsyncReadRepository<T>
    where T : BaseDomainModel
{
    private readonly MatchesDbContext _context;

    public ReadBaseRepository(MatchesDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, string? includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();
        if (!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>()!.FindAsync(id);
    }
}
