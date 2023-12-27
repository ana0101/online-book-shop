namespace OnlineBookShop.Models
{
    public class CartDto
    {
        public required string ApplicationUserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
