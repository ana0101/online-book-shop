using OnlineBookShop.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookShop.Models
{
    public class BookOrderDto
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
