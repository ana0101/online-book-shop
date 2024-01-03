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
        private readonly ICartsRepository _cartsRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BookOrdersController(
            IBookOrdersRepository bookOrdersRepository, 
            ICartsRepository cartsRepository, 
            IBooksRepository booksRepository, 
            IMapper mapper)
        {
            _bookOrdersRepository = bookOrdersRepository;
            _cartsRepository = cartsRepository;
            _booksRepository = booksRepository;
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

        [HttpPost("{applicationUserId}/{orderId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> PostBookOrdersAsync(string applicationUserId, int orderId)
        {
            // move the books from the user's cart to book order
            var carts = await _cartsRepository.GetCartsAsync(applicationUserId);
            foreach (var cart in carts)
            {
                var bookOrderDto = new BookOrderDto
                {
                    OrderId = orderId,
                    BookId = cart.BookId,
                    Price = await _booksRepository.GetBookPrice(cart.BookId),
                    Quantity = cart.Quantity
                };
                await PostBookOrderAsync(bookOrderDto);
            }
            return Ok();
        }
    }
}
