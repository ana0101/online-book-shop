using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public BooksController(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _booksRepository.GetBooksAsync();
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

        [HttpPost]
        public async Task<IActionResult> PostBook(BookDtoCreate bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _booksRepository.PostBookAsync(book);
            return Ok(book);
        }

        [HttpPut]
        public async Task<IActionResult> PutBook(int id, int newPrice)
        {
            var book = await _booksRepository.PutBookAsync(id, newPrice);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var ok = await _booksRepository.DeleteBookAsync(id);
            if (ok == false)
                return NotFound();
            return NoContent();
        }
    }
}
