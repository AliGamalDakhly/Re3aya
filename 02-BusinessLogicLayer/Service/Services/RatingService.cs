using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class RatingService : IRatingService
    {
        // this will be injected by DI container
        private readonly IUnitOfWork _unitOfWork;
        // this will be used to access the repository methods
        private readonly IGenericRepository<Rating, int> _context;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = _unitOfWork.Repository<Rating, int>();
        }

        public async Task<RatingDTO> AddRatingAsync(RatingDTO ratingDTO)
        {
            // Use Auto Mapper to map DTO to Entity
            var rating = _mapper.Map<Rating>(ratingDTO);

            // add your required logic here
            //...
            var result = await _context.AddAsync(rating);
            await _unitOfWork.CompleteAsync(); // it executes "SaveChanges"
            // Use Auto Mapper to map Entity to DTO
            return _mapper.Map<RatingDTO>(result);
        }

        public async Task<bool> UpdateRatingAsync(RatingDTO ratingDTO, int id)
        {
            // add your required logic here
            // ...
            // we first get the entity from DB.
            var existingRating = await _context.GetByIdAsync(id);
            if (existingRating == null)
                return false;
            //-------- dont forget to update all fields of your entity ---------
            existingRating.RatingValue = ratingDTO.RatingValue;
            existingRating.Comment = ratingDTO.Comment;
            existingRating.PatientId = ratingDTO.PatientId;
            existingRating.DoctorId = ratingDTO.DoctorId;
            await _context.UpdateAsync(existingRating);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<RatingDTO> GetRatingByIdAsync(int id)
        {
            // add your required logic here
            // ...
            var rating = await _context.GetByIdAsync(id);
            if (rating == null)
                return null;
            // Use Auto Mapper to map Entity to DTO
            return _mapper.Map<RatingDTO>(rating);
        }

        public async Task<bool> DeleteRatingByIdAsync(int id)
        {
            // add your required logic here
            // ...
            var rating = await _context.GetByIdAsync(id);
            if (rating == null)
                return false;
            await _context.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteRatingAsync(RatingDTO ratingDTO)
        {
            // add your required logic here
            // ...
            var rating = _mapper.Map<Rating>(ratingDTO);
            if (rating == null)
                return false;
            await _context.DeleteAsync(rating);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<int> CountAsync(Expression<Func<Rating, bool>>? filter = null)
        {
            // add your required logic here
            // ...
            return await _context.CountAsync(filter);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Rating, bool>> predicate)
        {
            // add your required logic here
            // ...
            return await _context.ExistsAsync(predicate);
        }

        public async Task<List<RatingDTO>> GetAllAsync(QueryOptions<Rating>? options = null)
        {
            // add your required logic here
            // ...
            var ratings = await _context.GetAllAsync(options);
            // Use Auto Mapper to map Entity to DTO
            return _mapper.Map<List<RatingDTO>>(ratings);
        }
    }
}
