using System.Text.Json.Serialization;

namespace OnlineBookShop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Price { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Carts { get; set; }
    }
}
