using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface ISystemInfoService
    {
        Task<double> GetBalance();
        Task<bool> UpdateBalance(double balance);
        Task<bool> IncrementBalance(double amount);
        Task<bool> DecrementBalance(double amount);
        Task<double> CalcBalance();
    }
}
