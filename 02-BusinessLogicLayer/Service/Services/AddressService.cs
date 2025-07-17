
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.Service.IServices;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AddressService : IAddressService
    {
        // this will be injected by DI container
        private readonly IUnitOfWork _unitOfWork;
        // this will be used to access the repository methods
        private readonly IGenericRepository<Government, int> _Governmentcontext;
        private readonly IGenericRepository<City, int> _cityContext;
        public AddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _Governmentcontext = _unitOfWork.Repository<Government, int>();
            _cityContext = _unitOfWork.Repository<City, int>();
        }

        public async Task<City> AddCityAsync(City city)
        {
            var addedCity = await _cityContext.AddAsync(city);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return addedCity;
        }

        public async Task<Government> AddGovernmentAsync(Government government)
        {
            var addedGovernment = await _Governmentcontext.AddAsync(government);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return addedGovernment;
        }

        public async Task<int> CountCitiesAsync(Expression<Func<City, bool>>? filter)
        {

            return await _cityContext.CountAsync(filter);

        }

        public async Task<int> CountGovernmentsAsync(Expression<Func<Government, bool>>? filter)
        {
            return await _Governmentcontext.CountAsync(filter);
        }

        public async Task<bool> ExistsCityAsync(Expression<Func<City, bool>> predicate)
        {
            return await _cityContext.ExistsAsync(predicate);
        }

        public async Task<bool> ExistsGovernmentAsync(Expression<Func<Government, bool>> predicate)
        {
            return await _Governmentcontext.ExistsAsync(predicate);
        }

        public async Task<List<Government>> GetAllGovernmentsAsync(QueryOptions<Government>? options)
        {
            return await _Governmentcontext.GetAllAsync(options);
        }

        public async Task<List<City>> GetAllCitiesAsync(QueryOptions<City>? options)
        {
            return await _cityContext.GetAllAsync(options);
        }

        public async Task<City> GetCityByIdAsync(int id)
        {
            return await _cityContext.GetByIdAsync(id);
        }

        public async Task<Government> GetGovernmentByIdAsync(int id)
        {
            return await _Governmentcontext.GetByIdAsync(id);
        }

        public async Task<bool> UpdateCityAsync(City city)
        {

            // we first get the entity from DB.
            var existingCity = await _cityContext.GetByIdAsync(city.CityId);
            if (existingCity == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingCity.Name = city.Name;
            existingCity.GovernmentId = city.GovernmentId;

            await _cityContext.UpdateAsync(existingCity);
            await _unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<bool> UpdateGovernmentAsync(Government government)
        {

            // we first get the entity from DB.
            var existingGovernment = await _Governmentcontext.GetByIdAsync(government.GovernmentId);
            if (existingGovernment == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingGovernment.Name = government.Name;
            await _Governmentcontext.UpdateAsync(existingGovernment);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
