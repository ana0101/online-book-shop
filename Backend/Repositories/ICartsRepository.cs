using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface ICartsRepository
    {
        public Task<IEnumerable<Cart>> GetCartsAsync(string applicationUserId);
        public Task<int> GetQuantityAsync(string applicationUserId, int bookId);
        public Task<Cart> PostCartAsync(Cart cart);
        public Task<Cart>? PutCartAsync(string applicationUserId, int bookId, int newQuantity);
        public Task<Boolean> DeleteCartAsync(string applicationUserId, int bookId);
    }
}
