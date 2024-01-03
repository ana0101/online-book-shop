using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface IBooksRepository
    {
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<Book>? GetBookAsync(int id);
        public Task<int> GetBookPrice(int id);
        public Task<Book> PostBookAsync(Book book);
        public Task<Book>? PutBookAsync(int id, int newPrice);
        public Task<Boolean> DeleteBookAsync(int id);
    }
}
