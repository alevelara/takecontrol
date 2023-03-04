using System.Collections;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Application.Contracts.Persitence.Emails;
using takecontrol.Application.Contracts.Persitence.Primitives;
using takecontrol.Domain.Primitives;
using takecontrol.EmailEngine.Persistence.Contexts;
using takecontrol.EmailEngine.Repositories.Emails;
using takecontrol.EmailEngine.Repositories.Primitives;

namespace takecontrol.Infrastructure.Repositories.Primitives;

public class EmailUnitOfWork : IEmailUnitOfWork
{
    private readonly EmailDbContext _context;
    private Hashtable _repostories;

    public EmailUnitOfWork(EmailDbContext context)
    {
        _context = context;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IAsyncWriteRepository<TEntity> Repository<TEntity>()
        where TEntity : BaseDomainModel
    {
        if (_repostories == null)
            _repostories = new Hashtable();

        var nameType = typeof(TEntity).Name;

        if (!_repostories.ContainsKey(nameType))
        {
            var repositoryType = typeof(WriteBaseRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repostories.Add(nameType, repositoryInstance);
        }

        return (IAsyncWriteRepository<TEntity>)_repostories[nameType];
    }

    public IEmailWriteRepository EmailWriteRepository()
    {
        return new EmailWriteRepository(_context);
    }
}
