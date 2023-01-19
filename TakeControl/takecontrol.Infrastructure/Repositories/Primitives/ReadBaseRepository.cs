﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Primitives;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories.Primitives;

public class ReadBaseRepository<T> : IAsyncReadRepository<T> where T : BaseDomainModel
{
    protected readonly TakeControlDbContext _context;

    public ReadBaseRepository(TakeControlDbContext context)
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

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, string includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = _context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();
        if (!string.IsNullOrEmpty(includeString)) query = query.Include(includeString);
        if (predicate != null) query = query.Where(predicate);
        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
}