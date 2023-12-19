using OnlineBookShop.Models;

namespace OnlineBookShop.Repositories
{
    public interface IBooksRepository
    {
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<Book>? GetBookAsync(int id);
        public Task<Book> PostBookAsync(Book book);
        public Task<Book>? PutBookAsync(int id, string newDescription);
        public Task<Boolean> DeleteBookAsync(int id);
    }
}
