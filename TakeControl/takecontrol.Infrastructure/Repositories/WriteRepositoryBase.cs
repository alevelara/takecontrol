using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Primitives;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories;

public class WriteRepositoryBase<T> : IAsyncWriteRepository<T> where T : BaseDomainModel
{
    protected readonly TakeControlDbContext _context;

    public WriteRepositoryBase(TakeControlDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove<T>(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}
