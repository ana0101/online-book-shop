using OnlineBookShop.Entities;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Models
{
    public class PaymentDto
    {
        public int OrderId { get; set; }
        public float Total {  get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentType Type { get; set; }
    }
}
