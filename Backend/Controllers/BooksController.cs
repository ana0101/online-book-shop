using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;
using OnlineBookShop.Repositories;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public BooksController(IBooksRepository booksRepository, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _booksRepository.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet("sortTitle/{order}")]
        public async Task<IActionResult> GetBooksSortedByTitle(Boolean order)
        {
            var books = await _booksRepository.GetBooksSortedByTitleAsync(order);
            return Ok(books);
        }

        [HttpGet("sortPrice/{order}")]
        public async Task<IActionResult> GetBooksSortedByPrice(Boolean order)
        {
            var books = await _booksRepository.GetBooksSortedByPriceAsync(order);
            return Ok(books);
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> GetBooksSearch(string search)
        {
            var books = await _booksRepository.GetBooksSearchAsync(search);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _booksRepository.GetBookAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpGet("price/{id}")]
        public async Task<int> GetBookPrice(int id)
        {
            var price = await _booksRepository.GetBookPrice(id);
            return price;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostBook(BookDtoCreate bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _booksRepository.PostBookAsync(book);
            return Ok(book);
        }

        [HttpPut("{id}/{newPrice}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook(int id, int newPrice)
        {
            var book = await _booksRepository.PutBookAsync(id, newPrice);
            if (book == null)
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "There is no book with this id" });
            return Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var ok = await _booksRepository.DeleteBookAsync(id);
            if (ok == false)
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "There is no book with this id" });
            return NoContent();
        }
    }
}
