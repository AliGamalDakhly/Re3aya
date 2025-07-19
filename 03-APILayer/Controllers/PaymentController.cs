
using _01_DataAccessLayer.UnitOfWork;
using _02_BusinessLogicLayer.DTOs.PaymentDTOs;
using _02_BusinessLogicLayer.Service.IServices;
using Microsoft.AspNetCore.Mvc;
  
namespace _03_APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymobService _paymobService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymobService paymobService, IUnitOfWork unitOfWork, ILogger<PaymentController> logger)
        {
            _paymobService = paymobService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost("book-appointment")]
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

 



    }
}