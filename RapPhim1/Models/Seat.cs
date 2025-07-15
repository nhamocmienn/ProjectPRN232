namespace RapPhim1.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public string Row { get; set; } = null!;
        public int Column { get; set; }
        public int SeatTypeId { get; set; }
        public SeatType SeatType { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }

}
