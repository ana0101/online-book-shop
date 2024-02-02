using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface IBooksRepository
    {
        public Task<IEnumerable<Book>> GetBooksAsync();
        public Task<IEnumerable<Book>> GetBooksSortedByTitleAsync(Boolean order);
        public Task<IEnumerable<Book>> GetBooksSortedByPriceAsync(Boolean order);
        public Task<IEnumerable<Book>> GetBooksSearchAsync(string search);
        public Task<Book>? GetBookAsync(int id);
        public Task<float> GetBookPrice(int id);
        public Task<Book> PostBookAsync(Book book);
        public Task<Book>? PutBookAsync(int id, float newPrice);
        public Task<Boolean> DeleteBookAsync(int id);
    }
}
