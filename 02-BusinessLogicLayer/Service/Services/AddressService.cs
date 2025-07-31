
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AddressDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System.Linq.Expressions;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AddressService : IAddressService
    {
        // this will be injected by DI container
        private readonly IUnitOfWork _unitOfWork;
        // this will be used to access the repository methods
        private readonly IGenericRepository<Government, int> _governmentContext;
        private readonly IGenericRepository<City, int> _cityContext;
        private readonly IGenericRepository<Address, int> _addressContext;
        private readonly IMapper _mapper;
        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _governmentContext = _unitOfWork.Repository<Government, int>();
            _cityContext = _unitOfWork.Repository<City, int>();
            _addressContext = _unitOfWork.Repository<Address, int>();
            _mapper = mapper;
        }

        public async Task<CityDTO> AddCityAsync(CityDTO city)
        {
            var cityEntity = _mapper.Map<City>(city);
            var addedCity = await _cityContext.AddAsync(cityEntity);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return _mapper.Map<CityDTO>(addedCity);
        }

        public async Task<GovernmentDTO> AddGovernmentAsync(GovernmentDTO government)
        {
            var governmentEntity = _mapper.Map<Government>(government);
            var addedGovernment = await _governmentContext.AddAsync(governmentEntity);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return _mapper.Map<GovernmentDTO>(addedGovernment);
        }

        public async Task<int> CountCitiesAsync(Expression<Func<City, bool>>? filter)
        {

            return await _cityContext.CountAsync(filter);

        }

        public async Task<int> CountGovernmentsAsync(Expression<Func<Government, bool>>? filter)
        {
            return await _governmentContext.CountAsync(filter);
        }

        public async Task<bool> ExistsCityAsync(Expression<Func<City, bool>> predicate)
        {
            return await _cityContext.ExistsAsync(predicate);
        }

        public async Task<bool> ExistsGovernmentAsync(Expression<Func<Government, bool>> predicate)
        {
            return await _governmentContext.ExistsAsync(predicate);
        }

        public async Task<List<GovernmentDTO>> GetAllGovernmentsAsync(QueryOptions<Government>? options)
        {
            List<Government> governments = await _governmentContext.GetAllAsync(options);
            List<GovernmentDTO> governmentDTOs = _mapper.Map<List<GovernmentDTO>>(governments);
            return governmentDTOs;
        }

        public async Task<List<CityDTO>> GetAllCitiesAsync(QueryOptions<City>? options)
        {
            List<City> cities = await _cityContext.GetAllAsync(options);
            List<CityDTO> cityDTOs = _mapper.Map<List<CityDTO>>(cities);
            return cityDTOs;
        }

        public async Task<CityDTO> GetCityByIdAsync(int id)
        {

            City city = await _cityContext.GetByIdAsync(id);

            return _mapper.Map<CityDTO>(city);
        }

        public async Task<GovernmentDTO> GetGovernmentByIdAsync(int id)
        {

            Government government = await _governmentContext.GetByIdAsync(id);
            return _mapper.Map<GovernmentDTO>(government);
        }

        public async Task<bool> UpdateCityAsync(CityDTO city, int id)
        {

            // we first get the entity from DB.
            var existingCity = await _cityContext.GetByIdAsync(id);

            if (existingCity == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingCity.Name = city.Name;
            existingCity.GovernmentId = city.GovernmentId;

            await _cityContext.UpdateAsync(existingCity);
            await _unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<bool> UpdateGovernmentAsync(GovernmentDTO government, int id)
        {

            // we first get the entity from DB.
            var existingGovernment = await _governmentContext.GetByIdAsync(id);
            if (existingGovernment == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingGovernment.Name = government.Name;
            await _governmentContext.UpdateAsync(existingGovernment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<GovernmentDTO> GetGovernmentByCityIdAsync(int id)
        {

            City city = await _cityContext.GetByIdAsync(id);
            if (city == null)
                return null;
            Government government = await _governmentContext.GetByIdAsync(city.GovernmentId);
            return _mapper.Map<GovernmentDTO>(government);
        }
        public async Task<AddressDTO> AddAddressAsync(AddressDTO address)
        {
            var addressEntity = _mapper.Map<Address>(address);

            var addedAddress = await _addressContext.AddAsync(addressEntity);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            return _mapper.Map<AddressDTO>(addedAddress);

        }

        public async Task<bool> UpdateAddressAsync(AddressDTO address, int id)
        {

            // we first get the entity from DB.
            var existingAddress = await _addressContext.GetByIdAsync(id);
            if (existingAddress == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingAddress.Location = address.Location;
            existingAddress.DetailedAddress = address.DetailedAddress;
            existingAddress.CityId = address.CityId;
            existingAddress.DoctorId = address.DoctorId;
            await _addressContext.UpdateAsync(existingAddress);
            await _unitOfWork.CompleteAsync();
            return true;

        }

        public async Task<bool> DeleteAddressAsync(AddressDTO address)
        {


            var addressEntity = _mapper.Map<Address>(address);
            return await _addressContext.DeleteAsync(addressEntity);

        }

        public async Task<bool> DeleteAddressByIdAsync(int id)
        {

            return await _addressContext.DeleteByIdAsync(id);
        }

        public async Task<List<AddressDTO>> GetAllAddressesAsync(QueryOptions<Address>? options = null)
        {

            List<Address> addresses = await _addressContext.GetAllAsync(options);
            List<AddressDTO> addressDTOs = _mapper.Map<List<AddressDTO>>(addresses);
            return addressDTOs;
        }

        public async Task<List<DetailedAddressDTO>> GetDetialedAddressesAsync(QueryOptions<Address>? options = null)
        {

            List<Address> addresses = await _addressContext.GetAllAsync(options);
            List<DetailedAddressDTO> addressDTOs = _mapper.Map<List<DetailedAddressDTO>>(addresses);
            return addressDTOs;
        }

        public async Task<AddressDTO> GetAddressByIdAsync(int id)
        {

            Address address = await _addressContext.GetByIdAsync(id);
            return _mapper.Map<AddressDTO>(address);
        }

        public async Task<int> CountAddressesAsync(Expression<Func<Address, bool>>? filter = null)
        {

            return await _addressContext.CountAsync(filter);
        }

        public async Task<bool> ExistsAddressAsync(Expression<Func<Address, bool>> predicate)
        {

            return await _addressContext.ExistsAsync(predicate);

        }
    }
}
