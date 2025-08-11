using _01_DataAccessLayer.Data.Context;
using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.PatientDTOs;
using _02_BusinessLogicLayer.DTOs.PaymentDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymobService _paymobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PaymentController> _logger;
        private readonly Re3ayaDbContext _context;


        public PaymentController(IPaymobService paymobService, IUnitOfWork unitOfWork, ILogger<PaymentController> logger, Re3ayaDbContext context)
        {
            _paymobService = paymobService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _context = context;

        }

        [HttpPost("Create")]
        public async Task<IActionResult> BookAppointment([FromBody] AppointmentPaymentRequest request)
        {
            var authToken = await _paymobService.GetAuthTokenAsync();
            var orderId = await _paymobService.CreateOrderAsync(authToken, request.Amount, request.Items);
            var paymentKey = await _paymobService.GetPaymentKeyAsync(authToken, orderId, request);
            var iframeUrl = _paymobService.GetIframeUrl(paymentKey);
            return Ok(new { iframeUrl });
        }

        [HttpPost("refund")]
        public async Task<IActionResult> Refund([FromBody] RefundRequest request)
        {
            if (request.AmountCents < 10000)
            {
                return BadRequest(new { error = "the refund amount must be at least 100 EGP" });
            }

            try
            {
                var refundResponse = await _paymobService.RefundAsync(request.TransactionId, request.AmountCents);
                return Ok(refundResponse.RootElement);
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCode(502, new { error = "external refund API failed", details = httpEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "something went wrong during refund", details = ex.Message });
            }
        }






 

        [HttpGet("paymob-callback")]
        public async Task<IActionResult> PaymobCallback([FromQuery] PaymobCallbackDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            double amount = Convert.ToDouble(dto.AmountCents) / 100.0;

            var payment = new _01_DataAccessLayer.Models.Payment
            {
                TransactionId = dto.Id,
                Amount = amount,
                Status = dto.Success ? PaymentStatus.Completed : PaymentStatus.Failed,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<_01_DataAccessLayer.Models.Payment, int>().AddAsync(payment);
            await _unitOfWork.CompleteAsync();

             return Ok(new { message = "payment saved successfully", paymentId = payment.PaymentId });
        }








        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _unitOfWork.Repository<_01_DataAccessLayer.Models.Payment, int>().GetAllAsync();
            return Ok(payments);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _unitOfWork.Repository<_01_DataAccessLayer.Models.Payment, int>()
                .GetFirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return NotFound(new { message = "payment not found" });

            return Ok(payment);
        }




        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PaymentDtoForUser>>> GetPaymentsByUserId(int userId)
        {
            var payments = await _context.Payments
                .Where(p => p.Appointment.PatientId == userId)
                .Select(p => new PaymentDtoForUser
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    Status = p.Status,
                    TransactionId = p.TransactionId,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            if (payments == null || !payments.Any())
                return NotFound("no payments found for this user");

            return Ok(payments);
        }




        [HttpPatch("update")]
        public async Task<IActionResult> UpdatePaymentStatus([FromBody] UpdatePaymentStatusDTO dto)
        {
            var paymentRepo = _unitOfWork.Repository<_01_DataAccessLayer.Models.Payment, int>();


            var payment = await paymentRepo.GetFirstOrDefaultAsync(p => p.TransactionId == dto.TransactionId);

            if (payment == null)
                return NotFound(new { error = "payment not found" });


            payment.Status = dto.Status;

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                message = "payment status updated successfully",
                paymentId = payment.PaymentId,
                newStatus = payment.Status.ToString()
            });
        }







    }
}