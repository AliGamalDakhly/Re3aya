using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.AccountDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new Exception("username or Password is invalid");

            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
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
                Roles = roles.ToList()
            };
        }



         


    }
}
