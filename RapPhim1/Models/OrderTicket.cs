namespace RapPhim1.Models
{
    public class OrderTicket
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; } = null!;

        public int SeatId { get; set; }
        public Seat Seat { get; set; } = null!;

        public decimal Price { get; set; }
    }

}
