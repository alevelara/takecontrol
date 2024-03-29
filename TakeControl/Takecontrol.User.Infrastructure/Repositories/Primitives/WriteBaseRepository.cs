﻿using Microsoft.EntityFrameworkCore;
using Takecontrol.Shared.Domain.Primitives;
using Takecontrol.User.Application.Primitives;
using Takecontrol.User.Infrastructure.Persistence.Postgresql.Contexts;

namespace Takecontrol.User.Infrastructure.Repositories.Primitives;

public class WriteBaseRepository<T> : IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    private readonly TakeControlDbContext _context;

    public WriteBaseRepository(TakeControlDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public T Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}
