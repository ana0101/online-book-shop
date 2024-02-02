using System.Text.Json.Serialization;

namespace OnlineBookShop.Entities
{
    public enum PaymentType
    {
        CreditCard,
        DebitCard,
        Cash
    }

    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
        public float Total { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentType Type { get; set; }
    }
}
