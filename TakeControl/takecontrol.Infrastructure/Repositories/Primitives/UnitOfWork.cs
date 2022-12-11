using System.Collections;
using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Primitives;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories.Primitives;

public sealed class UnitOfWork : IUnitOfWork
{
    private Hashtable _repostories;
    private readonly TakeControlDbContext _context;

    public UnitOfWork(TakeControlDbContext context)
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

    public IAsyncWriteRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
    {
        if (_repostories == null)
            _repostories = new Hashtable();

        var nameType = typeof(TEntity).Name;

        if (!_repostories.ContainsKey(nameType))
        {
            var repositoryType = typeof(WriteRepositoryBase<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repostories.Add(nameType, repositoryInstance);
        }

        return (IAsyncWriteRepository<TEntity>)_repostories[nameType];
    }
}
