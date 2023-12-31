namespace OnlineBookShop.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Total { get; set; }
        public string Type { get; set; }
    }
}
