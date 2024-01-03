using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ShopContext _shopContext;

        public BooksRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var books = await _shopContext.Books.ToListAsync();
            return books;
        }

        public async Task<Book>? GetBookAsync(int id)
        {
            var book = await _shopContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task<int> GetBookPrice(int id)
        {
            var book = await _shopContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return 0;
            return book.Price;
        }

        public async Task<Book> PostBookAsync(Book book)
        {
            _shopContext.Books.Add(book);
            await _shopContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book>? PutBookAsync(int id, int newPrice)
        {
            var book = await _shopContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return null;
            book.Price = newPrice;
            _shopContext.Books.Update(book);
            await _shopContext.SaveChangesAsync();
            return book;
        }

        public async Task<Boolean> DeleteBookAsync(int id)
        {
            var book = _shopContext.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) 
                return false;
            _shopContext.Remove(book);
            await _shopContext.SaveChangesAsync();
            return true;
        }
    }
}
