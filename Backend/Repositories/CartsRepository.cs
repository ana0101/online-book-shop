using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;
using System.Diagnostics;

namespace OnlineBookShop.Repositories
{
    public class CartsRepository : ICartsRepository
    {
        private readonly ShopContext _shopContext;

        public CartsRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<IEnumerable<Cart>> GetCartsAsync(string applicationUserId)
        {
            var carts = await _shopContext.Carts.Where(c => c.ApplicationUserId == applicationUserId).Include(c => c.Book).ToListAsync();
            return carts;
        }

        public async Task<int> GetQuantityAsync(string applicationUserId, int bookId)
        {
            var cart = await _shopContext.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId && c.BookId == bookId);
            if (cart == null)
                return 0;
            return cart.Quantity;
        }

        public async Task<Cart> PostCartAsync(Cart cart)
        {
            _shopContext.Carts.Add(cart);
            await _shopContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart>? PutCartAsync(string applicationUserId, int bookId, int newQuantity)
        {
            var cart = await _shopContext.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId && c.BookId == bookId);
            if (cart == null)
                return null;
            cart.Quantity = newQuantity;
            _shopContext.Carts.Update(cart);
            await _shopContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Boolean> DeleteCartAsync(string applicationUserId, int bookId)
        {
            var cart = await _shopContext.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == applicationUserId && c.BookId == bookId);
            if (cart == null)
                return false;
            _shopContext.Remove(cart);
            await _shopContext.SaveChangesAsync();
            return true;
        }
    }
}
