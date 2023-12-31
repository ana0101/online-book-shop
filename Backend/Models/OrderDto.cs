using OnlineBookShop.Entities;

namespace OnlineBookShop.Models
{
    public class OrderDto
    {
        public string ApplicationUserId { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
