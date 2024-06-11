using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneShopApi.Data;
using PhoneShopApi.Dto.Payment;
using PhoneShopApi.Interfaces.IRepository;
using PhoneShopApi.Mappers;
using PhoneShopApi.Models;

namespace PhoneShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(PhoneShopDbContext context, IPaymentRepository paymentRepo) : Controller
    {
        private readonly PhoneShopDbContext _context = context;
        private readonly IPaymentRepository _paymentRepo = paymentRepo;

        [HttpGet]
        [Route("AllPayment")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentRepo.GetAllPaymentAsync();
            var paymentsDto = payments.Select(p => p.ToPaymentDto());

            return Ok(paymentsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] int id)
        {
            var payment = await _paymentRepo.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            var paymentDto = payment.ToPaymentDto();

            return Ok(paymentDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostPayment([FromBody] CreatePaymentRequestDto createPaymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newPayment = createPaymentDto.ToPaymentFromCreatePaymentRequestDto();
            await _paymentRepo.CreatePaymentAsync(newPayment);

            return Ok(newPayment.ToPaymentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutPayment(
            [FromRoute] int id,
            [FromBody] CreatePaymentRequestDto createPaymentRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedPayment = await _paymentRepo.UpdatePaymentAsync(id, createPaymentRequestDto);
            if (updatedPayment == null)
            {
                return NotFound();
            }

            return Ok(updatedPayment.ToPaymentDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePaymentById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var deletedPayment = await _paymentRepo.DeletePaymentByIdAsync(id);
            if (deletedPayment == null)
            {
                return NotFound();
            }

            return Ok(deletedPayment.ToPaymentDto());
        }
    }
}
