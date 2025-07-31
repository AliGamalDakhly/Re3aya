
using System.Linq.Expressions;
using _01_DataAccessLayer.Data.Context;
using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;

namespace _01_DataAccessLayer.Repository.GenericRepository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        #region Props
        protected readonly Re3ayaDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region Ctor
        public GenericRepository(Re3ayaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        #endregion


        #region Async Methods
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            await _dbSet.AddAsync(entity);
            return entity;
        }


        public Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            _dbSet.Remove(entity);
            return Task.FromResult(true);
        }


        public async Task<bool> DeleteByIdAsync(TKey id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id cannot be null");

            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }


        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
                return await _dbSet.CountAsync(filter);

            return await _dbSet.CountAsync();
        }


        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                return await _dbSet.AnyAsync(predicate);

            return await _dbSet.AnyAsync();
        }


        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id cannot be null");

            return await _dbSet.FindAsync(id);
        }


        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }


        public async Task<List<TEntity>> GetAllAsync(QueryOptions<TEntity>? options = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (options != null)
            {
                if (options.Filter != null)
                    query = query.Where(options.Filter);


                if (options.Includes != null && options.Includes.Length > 0)
                    foreach (var include in options.Includes)
                        query = query.Include(include);


                if (options.OrderBy != null)
                {
                    query = options.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(options.OrderBy)
                        : query.OrderByDescending(options.OrderBy);
                }

                if (options.Skip.HasValue && options.Skip.Value > 0)
                    query = query.Skip(options.Skip.Value);

                if (options.Take.HasValue && options.Take.Value > 0)
                    query = query.Take(options.Take.Value);
            }

            return await query.ToListAsync();
        }

        //public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    if (predicate == null)
        //        throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");
        //    return await _dbSet.FirstOrDefaultAsync(predicate);
        //}

        public async Task<TEntity?> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null");
            IQueryable<TEntity> query = _dbSet.Where(predicate);
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllIncludeAsync(
         Expression<Func<TEntity, bool>> predicate,
          params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.Where(predicate);

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        #endregion

        #region Synchronous Methods
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            _dbSet.Add(entity);
            return entity;
        }


        public bool Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            _dbSet.Remove(entity);
            return true;
        }


        public bool DeleteById(TKey id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id cannot be null");

            var entity = _dbSet.Find(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return true;
        }


        public int Count(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
                return _dbSet.Count(filter);

            return _dbSet.Count();
        }


        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                return _dbSet.Any(predicate);

            return _dbSet.Any();
        }


        public TEntity GetById(TKey id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id cannot be null");

            return _dbSet.Find(id);
        }


        public TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");

            _dbSet.Update(entity);
            return entity;
        }


        public List<TEntity> GetAll(QueryOptions<TEntity> options)
        {
            IQueryable<TEntity> query = _dbSet;

            if (options.Filter != null)
                query = query.Where(options.Filter);


            if (options.Includes != null && options.Includes.Length > 0)
                foreach (var include in options.Includes)
                    query = query.Include(include);

            if (options.OrderBy != null)
            {
                query = options.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(options.OrderBy)
                    : query.OrderByDescending(options.OrderBy);
            }

            if (options.Skip.HasValue && options.Skip.Value > 0)
                query = query.Skip(options.Skip.Value);

            if (options.Take.HasValue && options.Take.Value > 0)
                query = query.Take(options.Take.Value);

            return query.ToList();
        }

        #endregion
    }
}