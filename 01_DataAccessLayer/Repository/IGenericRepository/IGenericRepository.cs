using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;

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
