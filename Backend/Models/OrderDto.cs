using OnlineBookShop.Entities;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Models
{
    public class OrderDto
    {
        public string ApplicationUserId { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
    }
}
