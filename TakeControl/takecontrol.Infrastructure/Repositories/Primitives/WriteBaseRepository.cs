using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.Domain.Primitives;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories.Primitives;

public class WriteBaseRepository<T> : IAsyncWriteRepository<T> where T : BaseDomainModel
{
    protected readonly TakeControlDbContext _context;

    public WriteBaseRepository(TakeControlDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}
