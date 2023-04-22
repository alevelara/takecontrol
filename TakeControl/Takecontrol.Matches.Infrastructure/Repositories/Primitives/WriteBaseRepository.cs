﻿using Microsoft.EntityFrameworkCore;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Infrastructure.Repositories.Primitives;

public class WriteBaseRepository<T> : IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    private readonly MatchesDbContext _context;

    public WriteBaseRepository(MatchesDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void DeleteAsync(T entity)
    {
        _context.Remove(entity);
    }

    public T UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}