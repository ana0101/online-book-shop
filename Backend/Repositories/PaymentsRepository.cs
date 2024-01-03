using Microsoft.EntityFrameworkCore;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly ShopContext _shopContext;

        public PaymentsRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<Payment> GetPaymentAsync(int orderId)
        {
            var payment = await _shopContext.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
            return payment;
        }

        public async Task<Payment> PostPaymentAsync(Payment payment)
        {
            await _shopContext.SaveChangesAsync();
            _shopContext.Payments.Add(payment);
            await _shopContext.SaveChangesAsync();
            return payment;
        }
    }
}
