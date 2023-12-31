using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;
using OnlineBookShop.Models;

namespace OnlineBookShop.Repositories
{
    public class BookOrdersRepository : IBookOrdersRepository
    {
        private readonly ShopContext _shopContext;

        public BookOrdersRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<IEnumerable<BookOrder>> GetBookOrdersAsync(int orderId)
        {
            var bookOrders = await _shopContext.BookOrders.Where(bo => bo.OrderId == orderId).Include(bo => bo.Book).ToListAsync();
            return bookOrders;
        }

        public async Task<BookOrder> GetBookOrderAsync(int orderId, int bookId)
        {
            var bookOrder = await _shopContext.BookOrders.FirstOrDefaultAsync(bo => bo.OrderId == orderId && bo.BookId == bookId);
            return bookOrder;
        }

        public async Task<BookOrder> PostBookOrderAsync(BookOrder bookOrder)
        {
            _shopContext.BookOrders.Add(bookOrder);
            await _shopContext.SaveChangesAsync();
            return bookOrder;
        }
    }
}
