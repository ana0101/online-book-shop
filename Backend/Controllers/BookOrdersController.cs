using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;
using OnlineBookShop.Repositories;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookOrdersController : ControllerBase
    {
        private readonly IBookOrdersRepository _bookOrdersRepository;
        private readonly IMapper _mapper;

        public BookOrdersController(IBookOrdersRepository bookOrdersRepository, IMapper mapper)
        {
            _bookOrdersRepository = bookOrdersRepository;
            _mapper = mapper;
        }

        [HttpGet("order/{orderId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetBookOrdersAsync(int orderId)
        {
            var bookOrders = await _bookOrdersRepository.GetBookOrdersAsync(orderId);
            return Ok(bookOrders);
        }

        [HttpGet("{orderId}/{bookId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetBookOrderAsync(int orderId, int bookId)
        {
            var bookOrder = await _bookOrdersRepository.GetBookOrderAsync(orderId, bookId);
            return Ok(bookOrder);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostBookOrderAsync(BookOrderDto bookOrderDto)
        {
            var bookOrder = _mapper.Map<BookOrder>(bookOrderDto);
            await _bookOrdersRepository.PostBookOrderAsync(bookOrder);
            return Ok(bookOrder);
        }
    }
}
