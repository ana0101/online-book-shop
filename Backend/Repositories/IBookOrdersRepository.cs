using OnlineBookShop.Entities;
using OnlineBookShop.Models;

namespace OnlineBookShop.Repositories
{
    public interface IBookOrdersRepository
    {
        public Task<IEnumerable<BookOrder>> GetBookOrdersAsync(int orderId);
        public Task<BookOrder> GetBookOrderAsync(int orderId, int bookId);
        public Task<BookOrder> PostBookOrderAsync(BookOrder bookOrder);
    }
}
