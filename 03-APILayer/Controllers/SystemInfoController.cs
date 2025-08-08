using System.Threading.Tasks;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemInfoController : ControllerBase
    {
        private readonly ISystemInfoService _systemInfoService;

        public SystemInfoController(ISystemInfoService systemInfoService)
        {
            _systemInfoService = systemInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBalance()
        {
            try
            {
                double balance =  await _systemInfoService.GetBalance();
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPut("IncrementBalance")]
        public async Task<IActionResult> IncrementBalance([FromBody] double amount)
        {
            try
            {
                bool isSuccess = await _systemInfoService.IncrementBalance(amount);
                return Ok(isSuccess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPut("DecrementBalance")]
        public async Task<IActionResult> DecrementBalance([FromBody] double amount)
        {
            try
            {
                bool isSuccess = await _systemInfoService.DecrementBalance(amount);
                return Ok(isSuccess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPut("UpdateBalance")]
        public async Task<IActionResult> UpdateBalance([FromBody] double balance)
        {
            try
            {
                bool isSuccess = await _systemInfoService.UpdateBalance(balance);
                return Ok(isSuccess);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error: " + ex.Message);
            }
        }

        [HttpPut("CalcBalance")]
        public async Task<IActionResult> CalcBalance()
        {
            try
            {
                double balance = await _systemInfoService.CalcBalance();
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error: " + ex.Message);
            }
        }
    }
}
