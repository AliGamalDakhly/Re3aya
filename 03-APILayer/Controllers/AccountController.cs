using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {

            if (register.IsDoctor == true)
            {
                var doctorDto = new DoctorRegisterDTO
                {
                    FullName = register.FullName,
                    Email = register.Email,
                    Password = register.Password,
                    PhoneNumber = register.PhoneNumber,
                    UserName = register.UserName
                };
                var result = await _accountService.RegisterDoctorAsync(doctorDto);
                return Ok(result);
            }
            else if (register.IsDoctor == false)
            {
                var patientDto = new PatientRegisterDTO
                {
                    FullName = register.FullName,
                    Email = register.Email,
                    Password = register.Password,
                    PhoneNumber = register.PhoneNumber,
                    UserName = register.UserName
                };
                var result = await _accountService.RegisterPatientAsync(patientDto);
                return Ok(result);
            }
            else
            {
                return BadRequest("Invalid registration type specified.");
            }
        }

        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromBody] DoctorRegisterDTO dto)
        {
            var result = await _accountService.RegisterDoctorAsync(dto);
            return Ok(result);
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDTO dto)
        {
            var result = await _accountService.RegisterPatientAsync(dto);
            return Ok(result);
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDTO dto)
        {
            var result = await _accountService.RegisterAdminAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var response = await _accountService.LoginAsync(dto);
            return Ok(response);
        }
    }
}
