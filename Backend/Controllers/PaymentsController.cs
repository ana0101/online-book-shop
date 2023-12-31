using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;
using OnlineBookShop.Repositories;
using System.Runtime.InteropServices;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentsRepository paymentsRepository, IMapper mapper)
        {
            _paymentsRepository = paymentsRepository;
            _mapper = mapper;
        }

        [HttpGet("{orderId}")]
        [Authorize (Roles = "User")]
        public async Task<IActionResult> GetPaymentAsync(int orderId)
        {
            var payment = await _paymentsRepository.GetPaymentAsync(orderId);
            if (payment == null) 
                return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostPaymentAsync(PaymentDto paymentDto)
        {
            var payment = _mapper.Map<Payment>(paymentDto);
            await _paymentsRepository.PostPaymentAsync(payment);
            return Ok(payment);
        }
    }
}
