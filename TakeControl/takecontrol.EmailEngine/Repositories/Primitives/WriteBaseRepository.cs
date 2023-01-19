using Microsoft.EntityFrameworkCore;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Primitives;
using takecontrol.EmailEngine.Persistence.Contexts;

namespace takecontrol.EmailEngine.Repositories.Primitives;

public class WriteBaseRepository<T> : IAsyncWriteRepository<T> where T : BaseDomainModel
{
    private readonly EmailDbContext _context;

    public WriteBaseRepository(EmailDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}
