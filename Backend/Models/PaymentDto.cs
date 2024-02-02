using OnlineBookShop.Entities;

namespace OnlineBookShop.Models
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public float Total {  get; set; }
        public string Type { get; set; }
    }
}
