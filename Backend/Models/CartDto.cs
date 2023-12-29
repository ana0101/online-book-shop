namespace OnlineBookShop.Models
{
    public class CartDto
    {
        public string ApplicationUserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
