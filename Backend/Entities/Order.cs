﻿using System.Text.Json.Serialization;

namespace OnlineBookShop.Entities
{
    public enum OrderStatus
    {
        Placed,
        Shipping,
        Done
    }

    public class Order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public DateTime Date {  get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }

        public ICollection<BookOrder>? BookOrders { get; set; }

        public Payment? Payment { get; set; }
    }
}
