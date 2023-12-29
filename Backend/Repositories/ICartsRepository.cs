using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface ICartsRepository
    {
        public Task<IEnumerable<Cart>> GetCartsAsync(string ApplicationUserId);
        public Task<Cart>? GetCartAsync(int id);
        public Task<Cart> PostCartAsync(Cart cart);
        public Task<Cart>? PutCartAsync(int id, int newQuantity);
        public Task<Boolean> DeleteCartAsync(int id);
    }
}
