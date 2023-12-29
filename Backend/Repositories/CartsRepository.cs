using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;

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

        public async Task<Cart>? GetCartAsync(int id)
        {
            var cart = await _shopContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
            return cart;
        }

        public async Task<Cart> PostCartAsync(Cart cart)
        {
            _shopContext.Carts.Add(cart);
            await _shopContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart>? PutCartAsync(int id, int newQuantity)
        {
            var cart = await _shopContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
                return null;
            cart.Quantity = newQuantity;
            _shopContext.Carts.Update(cart);
            await _shopContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Boolean> DeleteCartAsync(int id)
        {
            var cart = _shopContext.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;
            _shopContext.Remove(cart);
            await _shopContext.SaveChangesAsync();
            return true;
        }
    }
}
