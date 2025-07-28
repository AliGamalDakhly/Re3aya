using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _02_BusinessLogicLayer.DTOs.RatingDTOs;
using _02_BusinessLogicLayer.DTOs.SpecailzationDTOs;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IRatingService
    {
        Task<RatingDTO> AddRatingAsync(RatingDTO ratingDTO);
        Task<bool> UpdateRatingAsync(RatingDTO ratingDTO, int id);
        Task<RatingDTO> GetRatingByIdAsync(int id);
        Task<bool> DeleteRatingByIdAsync(int id);// change int with primary key type of your entity
        Task<bool> DeleteRatingAsync(RatingDTO ratingDTO);
        Task<int> CountAsync(Expression<Func<Rating, bool>>? filter = null);
        Task<bool> ExistsAsync(Expression<Func<Rating, bool>> predicate);
        Task<List<RatingDTO>> GetAllAsync(QueryOptions<Rating>? options = null);
        Task<List<DoctorRatingDTO>> GetAllDoctorRatings(int doctorId);
    }
}
