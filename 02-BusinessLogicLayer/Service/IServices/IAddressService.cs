using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAddressService
    {
        #region Governments

        Task<GovernmentDTO> AddGovernmentAsync(GovernmentDTO government);

        Task<bool> UpdateGovernmentAsync(GovernmentDTO government, int id);

        Task<List<GovernmentDTO>> GetAllGovernmentsAsync(QueryOptions<Government>? options = null);

        Task<GovernmentDTO> GetGovernmentByIdAsync(int id);

        Task<int> CountGovernmentsAsync(Expression<Func<Government, bool>>? filter = null);

        Task<bool> ExistsGovernmentAsync(Expression<Func<Government, bool>> predicate);
        #endregion

        #region Cities
        Task<CityDTO> AddCityAsync(CityDTO city);

        Task<bool> UpdateCityAsync(CityDTO city, int id);

        Task<List<CityDTO>> GetAllCitiesAsync(QueryOptions<City>? options = null);

        Task<CityDTO> GetCityByIdAsync(int id);

        Task<int> CountCitiesAsync(Expression<Func<City, bool>>? filter = null);

        Task<bool> ExistsCityAsync(Expression<Func<City, bool>> predicate);
        #endregion

        #region Addresses
        Task<AddressDTO> AddAddressAsync(AddressDTO address);
        Task<bool> UpdateAddressAsync(AddressDTO address, int id);
        Task<bool> DeleteAddressAsync(AddressDTO address);
        Task<bool> DeleteAddressByIdAsync(int id);
        Task<List<AddressDTO>> GetAllAddressesAsync(QueryOptions<Address>? options = null);
        //Task<List<DetailedAddressDTO>> GetDetailedAddressesAsync(QueryOptions<Address>? options = null);
        Task<AddressDTO> GetAddressByIdAsync(int id);
        Task<int> CountAddressesAsync(Expression<Func<Address, bool>>? filter = null);
        Task<bool> ExistsAddressAsync(Expression<Func<Address, bool>> predicate);

        #endregion
    }
}
