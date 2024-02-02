using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface IOrdersRepository
    {
        public Task<Dictionary<string, List<Order>>> GetAllOrdersGroupedByUserAsync();
        public Task<Dictionary<string, List<Order>>> GetStatusOrdersGroupedByUserAsync(OrderStatus status);
        public Task<IEnumerable<Order>> GetOrdersAsync(string ApplicationUserId);
        public Task<Order> PostOrderAsync(Order order);
        public Task<Order>? PutOrderAsync(int id, OrderStatus newStatus);
        public Task<Boolean> DeleteOrderAsync(int id);
    }
}
