using System.Collections;
using Takecontrol.Matches.Infrastructure.Persistence.Postgresql.Contexts;
using Takecontrol.Shared.Application.Contracts.Persitence.Primitives;
using Takecontrol.Shared.Domain.Primitives;

namespace Takecontrol.Matches.Infrastructure.Repositories.Primitives;

public class UnitOfWork : IUnitOfWork
{
    private readonly MatchesDbContext _context;
    private Hashtable _repostories;

    public UnitOfWork(MatchesDbContext context)
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

        return (IAsyncWriteRepository<TEntity>)_repostories[nameType]!;
    }
}
