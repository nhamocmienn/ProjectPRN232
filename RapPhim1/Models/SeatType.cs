namespace RapPhim1.Models
{

    public class SeatType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal ExtraFee { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
