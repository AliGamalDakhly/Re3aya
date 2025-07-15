using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Repository.IGenericRepository;

namespace _01_DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class;

        int Complete();
        Task<int> CompleteAsync();

    }
}
