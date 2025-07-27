using System.Linq.Expressions;

namespace _01_DataAccessLayer.Repository.IGenericRepository
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {

        #region Async Methods
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteByIdAsync(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync(QueryOptions<TEntity>? options = null);

        //I will add this method to Enhance loading
        Task<TEntity?> GetFirstOrDefaultAsync(
           Expression<Func<TEntity, bool>> predicate,
           params Expression<Func<TEntity, object>>[] includes
        );
        //Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Synchronous Methods
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
        bool DeleteById(TKey id);
        TEntity GetById(TKey id);
        int Count(Expression<Func<TEntity, bool>>? filter = null);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> GetAll(QueryOptions<TEntity> options);
        #endregion

    }
}