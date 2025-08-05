using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DoctorService _doctorService;
        private readonly SignInManager<AppUser> _signInManager;

        private Doctor doctorInfo;
        private Patient patientInfo;
        public AccountService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<string> RegisterDoctorAsync(DoctorRegisterDTO dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // instead of seeding roles in program.cs, we can do it here

            //if (!await _roleManager.RoleExistsAsync("Doctor"))
            //    await _roleManager.CreateAsync(new IdentityRole("Doctor"));

            await _userManager.AddToRoleAsync(user, "Doctor");

            var doctor = _mapper.Map<Doctor>(dto);
            doctor.AppUserId = user.Id;

            await _unitOfWork.Repository<Doctor, int>().AddAsync(doctor);
            await _unitOfWork.CompleteAsync();

            return "Doctor registered successfully.";
        }

        public async Task<string> RegisterPatientAsync(PatientRegisterDTO dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // instead of seeding roles in program.cs, we can do it here

            //if (!await _roleManager.RoleExistsAsync("Patient"))
            //    await _roleManager.CreateAsync(new IdentityRole("Patient"));

            await _userManager.AddToRoleAsync(user, "Patient");

            var patient = _mapper.Map<Patient>(dto);
            patient.AppUserId = user.Id;

            await _unitOfWork.Repository<Patient, int>().AddAsync(patient);
            await _unitOfWork.CompleteAsync();

            return "Patient registered successfully.";

        }

        public async Task<string> RegisterAdminAsync(AdminRegisterDTO dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            // instead of seeding roles in program.cs, we can do it here

            //if (!await _roleManager.RoleExistsAsync("Admin"))
            //    await _roleManager.CreateAsync(new IdentityRole("Admin"));

            await _userManager.AddToRoleAsync(user, "Admin");

            var admin = new Admin
            {
                AppUserId = user.Id,
                SystemId = dto.SystemId
            };

            await _unitOfWork.Repository<Admin, int>().AddAsync(admin);
            await _unitOfWork.CompleteAsync();

            return "Admin registered successfully.";
        }



        //public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
        //{
        //    var user = await _userManager.FindByNameAsync(dto.UserName);
        //    if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
        //        throw new Exception("Username or Passowrd is invalid.");

        //    var roles = await _userManager.GetRolesAsync(user);

        //    var authClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim(ClaimTypes.NameIdentifier, user.Id),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //    };

        //    foreach (var role in roles)
        //        authClaims.Add(new Claim(ClaimTypes.Role, role));

        //    var authSigningKey = new SymmetricSecurityKey(
        //        Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

        //    var token = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Audience"],
        //        expires: DateTime.UtcNow.AddHours(3),
        //        claims: authClaims,
        //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //    );

        //    return new LoginResponseDTO
        //    {
        //        Token = new JwtSecurityTokenHandler().WriteToken(token),
        //        Expiration = token.ValidTo
        //    };
        //}



        public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if (user == null)
                throw new Exception("اسم المستخدم أو كلمة المرور غير صحيحة");

            var result = await _signInManager.PasswordSignInAsync(
                dto.UserName,
                dto.Password,
                isPersistent: false,
                lockoutOnFailure: true
            );

            if (result.IsLockedOut)
                throw new Exception("تم قفل الحساب مؤقتًا. حاول لاحقًا.");

            if (!result.Succeeded)
                throw new Exception("اسم المستخدم أو كلمة المرور غير صحيحة");

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault();


            int? patientId = null;
            int? doctorId = null;
            if (role == "Doctor")
            {
                doctorInfo = await _unitOfWork.Repository<Doctor, int>().GetFirstOrDefaultAsync(d => d.AppUserId == user.Id);

                doctorId = doctorInfo.DoctorId;
                //if (doctor == null)
                //    throw new Exception("Doctor profile not found.");
            }
            else if (role == "Patient")
            {
                patientInfo = await _unitOfWork.Repository<Patient, int>().GetFirstOrDefaultAsync(p => p.AppUserId == user.Id);
                patientId = patientInfo.PatientId;
                //if (patient == null)
                //    throw new Exception("Patient profile not found.");

            }


            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

    };

            //foreach (var role in roles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:IssuerIP"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new LoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList(),
                doctorinfo = doctorId,
                patientinfo = patientId
            };
        }






    }
}