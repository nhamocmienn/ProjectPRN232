namespace RapPhim1.Models
{
    public class OrderService
    {
        public int Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ServiceItemId { get; set; }
        public ServiceItem ServiceItem { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
