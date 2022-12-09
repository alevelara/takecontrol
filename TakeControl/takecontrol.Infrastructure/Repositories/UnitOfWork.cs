using takecontrol.Application.Contracts.Persitence;
using takecontrol.Domain.Primitives;
using takecontrol.Identity;

namespace takecontrol.Infrastructure.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
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
            throw new NotImplementedException();
        }
    }
}
