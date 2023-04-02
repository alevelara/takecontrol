using Microsoft.EntityFrameworkCore;
using Takecontrol.Emails.Infrastructure.Contexts;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Emails.Infrastructure.Repositories.Primitives;

public class WriteBaseRepository<T> : IAsyncWriteRepository<T>
    where T : BaseDomainModel
{
    private readonly EmailDbContext _context;

    public WriteBaseRepository(EmailDbContext context)
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
        _context.Set<T>().Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}
