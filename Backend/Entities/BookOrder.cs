using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Entities
{
    public class BookOrder
    {
        [Key]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BookId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
        public Book? Book { get; set; }
        // because the price of a book can change, I have to memorize the price at which the book was bought
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
