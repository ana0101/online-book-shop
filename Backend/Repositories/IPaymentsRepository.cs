using OnlineBookShop.Entities;

namespace OnlineBookShop.Repositories
{
    public interface IPaymentsRepository
    {
        public Task<Payment> GetPaymentAsync(int orderId);
        public Task<Payment> PostPaymentAsync(Payment payment);
    }
}
