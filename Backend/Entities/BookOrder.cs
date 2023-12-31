using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Order Order { get; set; }
        public Book Book { get; set; }
        // because the price of a book can change, I have to memorize the price at which the book was bought
        public int price { get; set; }
        public int quantity { get; set; }
    }
}
