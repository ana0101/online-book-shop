using System.Text.Json.Serialization;

namespace OnlineBookShop.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
        public float Total { get; set; }
        public string Type { get; set; }
    }
}
