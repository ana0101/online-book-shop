using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;
using System.Linq;

namespace OnlineBookShop.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ShopContext _shopContext;

        public OrdersRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<Dictionary<string, List<Order>>> GetAllOrdersGroupedByUserAsync()
        {
            var orders = await _shopContext.Orders.Include(o => o.BookOrders).ThenInclude(bo => bo.Book).Include(o => o.Payment).ToListAsync();
            var groupedOrders = orders.GroupBy(o => o.ApplicationUserId).ToDictionary(group => group.Key, group => group.ToList());
            return groupedOrders;
        }

        public async Task<Dictionary<string, List<Order>>> GetStatusOrdersGroupedByUserAsync(string status)
        {
            var orders = await _shopContext.Orders.Where(o => o.Status == status).Include(o => o.BookOrders).ThenInclude(bo => bo.Book).Include(o => o.Payment).ToListAsync();
            var groupedOrders = orders.GroupBy(o => o.ApplicationUserId).ToDictionary(group => group.Key, group => group.ToList());
            return groupedOrders;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string applicationUserId)
        {
            var orders = await _shopContext.Orders.Where(o => o.ApplicationUserId == applicationUserId).Include(o => o.BookOrders)
            .ThenInclude(bo => bo.Book).Include(o => o.Payment).ToListAsync();
            return orders;
        }

        public async Task<Order> PostOrderAsync(Order order)
        {
            _shopContext.Orders.Add(order);
            await _shopContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> PutOrderAsync(int id, string newStatus)
        {
            var order = await _shopContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                return null;
            order.Status = newStatus;
            _shopContext.Orders.Update(order);
            await _shopContext.SaveChangesAsync();
            return order;
        }

        public async Task<Boolean> DeleteOrderAsync(int id)
        {
            var order = _shopContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                return false;
            _shopContext.Orders.Remove(order);
            await _shopContext.SaveChangesAsync();
            return true;
        }
    }
}
