using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IAccountService
    {
        Task<string> RegisterDoctorAsync(DoctorRegisterDTO dto);
        Task<string> RegisterPatientAsync(PatientRegisterDTO dto);
        Task<string> RegisterAdminAsync(AdminRegisterDTO dto);
        Task<LoginResponseDTO> LoginAsync(LoginDTO dto);
    }
}
