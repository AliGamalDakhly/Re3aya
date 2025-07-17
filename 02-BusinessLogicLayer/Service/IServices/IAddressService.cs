using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAddressService
    {
        #region Governments

        Task<Government> AddGovernmentAsync(Government government);

        Task<bool> UpdateGovernmentAsync(Government government);

        Task<List<Government>> GetAllGovernmentsAsync(QueryOptions<Government>? options = null);

        Task<Government> GetGovernmentByIdAsync(int id);

        Task<int> CountGovernmentsAsync(Expression<Func<Government, bool>>? filter = null);

        Task<bool> ExistsGovernmentAsync(Expression<Func<Government, bool>> predicate);
        #endregion

        #region Cities
        Task<City> AddCityAsync(City city);

        Task<bool> UpdateCityAsync(City city);

        Task<List<City>> GetAllCitiesAsync(QueryOptions<City>? options = null);

        Task<City> GetCityByIdAsync(int id);

        Task<int> CountCitiesAsync(Expression<Func<City, bool>>? filter = null);

        Task<bool> ExistsCityAsync(Expression<Func<City, bool>> predicate);
        #endregion
    }
}
