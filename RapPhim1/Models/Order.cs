namespace RapPhim1.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int? UserId { get; set; }  // null nếu mua tại quầy
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Paid, Success, Failed
        public decimal TotalAmount { get; set; }

        public ICollection<OrderTicket> OrderTickets { get; set; } = new List<OrderTicket>();
        public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
    }
}
