using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.DTOs.AdminDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDTO>> GetAllAdminsAsync();
        Task<AdminDTO> GetAdminByIdAsync(int id);
        Task<bool> UpdateAdminAsync(int id, AdminUpdateDTO dto);
        Task<bool> DeleteAdminAsync(int id);
    }
}
