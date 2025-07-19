using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.TimeSlotDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TimeSlot,int> _genericRepository;
        private readonly IMapper _mapper;

        // injection 
        public TimeSlotService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _genericRepository = _unitOfWork.Repository<TimeSlot,int>();

        }

        //add TimeSlot
        public async Task<TimeSlotDTO> AddAsync(CreateTimeSlotDTO dto)
        {
            var entity=_mapper.Map<TimeSlot>(dto);
            var result = await _genericRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<TimeSlotDTO>(result);
        }

        //count
        public async Task<int> CountAsync(Expression<Func<TimeSlot, bool>>? filter = null)
        {
            return await _genericRepository.CountAsync(filter);
        }

        //delete entity return True or False
        public async Task<bool> DeleteAsync(EditTimeSlotDTO dto)
        {
            var entity = await _genericRepository.GetByIdAsync(dto.TimeSlotId);
            if (entity == null)
            {
                return false;
            }
            var result = await _genericRepository.DeleteAsync(entity);
            await _unitOfWork.CompleteAsync();
            return result;

        }


        //delete entity by id
        public async Task<bool> DeleteByIdAsync(int id)
        {

            var result = await _genericRepository.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;


            //or 

            //var entity =await _genericRepository.GetByIdAsync(id);
            //if (entity == null)
            //{
            //    return false;
            //}
            //var result = await _genericRepository.DeleteAsync(entity);
            //await _unitOfWork.CompleteAsync();
            //return result;
        }

        //is exist?
        public Task<bool> ExistsAsync(Expression<Func<TimeSlot, bool>> predicate)
        {
            return _genericRepository.ExistsAsync(predicate);
        }

        //get all
        public async Task<List<TimeSlotDTO>> GetAllAsync(QueryOptions<TimeSlot> options)
        {
            var list = await _genericRepository.GetAllAsync(options);
            return _mapper.Map<List<TimeSlotDTO>>(list);

        }

        //get by id
        public async Task<TimeSlotDTO> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            return _mapper.Map<TimeSlotDTO>(entity);

        }


        //upate / edit entity
        public async Task<bool> UpdateAsync(EditTimeSlotDTO dto)
        {
            var entity = await _genericRepository.GetByIdAsync(dto.TimeSlotId);
            if (entity == null)
            {
                return false;
            }
            _mapper.Map(dto, entity);

            await _genericRepository.UpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
