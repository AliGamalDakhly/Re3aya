using _01_DataAccessLayer.Data.Context;
using _01_DataAccessLayer.Repository.GenericRepository;
using _01_DataAccessLayer.Repository.IGenericRepository;

namespace _01_DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Re3ayaDbContext _context;

        // Cache of repositories
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(Re3ayaDbContext context)
        {
            _context = context;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class
        {
            var type = typeof(TEntity);

            // Check if already created
            if (!_repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity, TKey>(_context);
                _repositories[type] = repo;
            }

            return (IGenericRepository<TEntity, TKey>)_repositories[type];
        }

    }
}
