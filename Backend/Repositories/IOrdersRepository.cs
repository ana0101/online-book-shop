using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface IOrdersRepository
    {
        public Task<IEnumerable<Order>> GetOrdersAsync(string ApplicationUserId);
        public Task<Order>? GetOrderAsync(int id);
        public Task<Order> PostOrderAsync(Order order);
        public Task<Order>? PutOrderAsync(int id, string newStatus);
        public Task<Boolean> DeleteOrderAsync(int id);
    }
}
