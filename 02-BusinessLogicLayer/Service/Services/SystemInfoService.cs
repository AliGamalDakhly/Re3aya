using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.Repository;
using _01_DataAccessLayer.Repository.IGenericRepository;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.Service.IServices;
using AutoMapper;

namespace _02_BusinessLogicLayer.Service.Services
{
    public class SystemInfoService : ISystemInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<SystemInfo, int> _context;
        private readonly IMapper _mapper;

        public SystemInfoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = _unitOfWork.Repository<SystemInfo, int>();
            _mapper = mapper;
        }

        public async Task<double> GetBalance()
        {
            SystemInfo systemInfo = await _context.GetByIdAsync(2);
            if (systemInfo == null)
                throw new Exception("System information not found.");
            return systemInfo.Balance;
        }

        public async Task<bool> UpdateBalance(double balance)
        {
            SystemInfo systemInfo = await _context.GetByIdAsync(2);
            if (systemInfo == null)
                throw new Exception("System information not found.");
            systemInfo.Balance = balance;

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> IncrementBalance(double amount)
        {
            SystemInfo systemInfo = await _context.GetByIdAsync(2);
            if (systemInfo == null)
                throw new Exception("System information not found.");
            systemInfo.Balance += amount;

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DecrementBalance(double amount)
        {
            SystemInfo systemInfo = await _context.GetByIdAsync(2);
            if (systemInfo == null)
                throw new Exception("System information not found.");
            systemInfo.Balance -= amount;

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<double> CalcBalance()
        {
            List<Doctor> doctors = await _unitOfWork.Repository<Doctor, int>().GetAllAsync();

            double totalBalance = 0;

            foreach (var doctor in doctors)
            {
                totalBalance += (.1 * (doctor.Balance / .9));
            }

            SystemInfo systemInfo = await _context.GetByIdAsync(2);
            if (systemInfo == null)
                throw new Exception("System information not found.");

            systemInfo.Balance = totalBalance;
            await _unitOfWork.CompleteAsync();
            return systemInfo.Balance;
        }
    }
}
