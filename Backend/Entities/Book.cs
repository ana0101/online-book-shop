using System.Text.Json.Serialization;

namespace OnlineBookShop.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Carts { get; set; }

        [JsonIgnore]
        public ICollection<BookOrder> BookOrders { get; set; }
    }
}
