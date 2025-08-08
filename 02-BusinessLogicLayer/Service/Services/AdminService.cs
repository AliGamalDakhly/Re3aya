using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.AdminDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<AdminDTO>> GetAllAdminsAsync()
        {
            var options = new QueryOptions<Admin>
            {
                Includes = new Expression<Func<Admin, object>>[] { a => a.AppUser }
            };

            var admins = await _unitOfWork.Repository<Admin, int>().GetAllAsync(options);
            return admins.Select(admin => new AdminDTO
            {
                AdminId = admin.AdminId,
                AppUserId = admin.AppUserId,
                FullName = admin.AppUser.FullName,
                Email = admin.AppUser.Email,
                PhoneNumber = admin.AppUser.PhoneNumber,
                DateOfBirth = admin.AppUser.DateOfBirth,
                Gender = admin.AppUser.Gender,
                SystemId = admin.SystemId
            });
        }

        public async Task<AdminDTO> GetAdminByIdAsync(int id)
        {
            var admin = await _unitOfWork.Repository<Admin, int>()
                .GetFirstOrDefaultAsync(a => a.AdminId == id, new Expression<Func<Admin, object>>[] { a => a.AppUser });
            if (admin == null) return null;

            return new AdminDTO
            {
                AdminId = admin.AdminId,
                AppUserId = admin.AppUserId,
                FullName = admin.AppUser.FullName,
                Email = admin.AppUser.Email,
                PhoneNumber = admin.AppUser.PhoneNumber,
                DateOfBirth = admin.AppUser.DateOfBirth,
                Gender = admin.AppUser.Gender,
                SystemId = admin.SystemId
            };
        }


        public async Task<bool> UpdateAdminAsync(int id, AdminUpdateDTO dto)
        {
            var admin = await _unitOfWork.Repository<Admin, int>()
                .GetFirstOrDefaultAsync(a => a.AdminId == id, new Expression<Func<Admin, object>>[] { a => a.AppUser });

            if (admin == null) return false;

            admin.AppUser.FullName = dto.FullName;
            admin.AppUser.Email = dto.Email;
            admin.AppUser.PhoneNumber = dto.PhoneNumber;
            admin.AppUser.DateOfBirth = dto.DateOfBirth;
            admin.AppUser.Gender = dto.Gender;
            admin.SystemId = dto.SystemId;

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await _unitOfWork.Repository<Admin, int>().GetByIdAsync(id);
            if (admin == null) return false;

            var user = await _userManager.FindByIdAsync(admin.AppUserId);
            if (user != null)
                await _userManager.DeleteAsync(user); // Will also remove admin because of FK

            await _unitOfWork.CompleteAsync();
            return true;
        }
    }

}
