using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBookShop.Entities
{
    public class Cart
    {
        [Key]
        [Column(Order = 0)]
        public string ApplicationUserId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int BookId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
