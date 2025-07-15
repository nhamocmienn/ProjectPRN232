namespace RapPhim1.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsActive { get; set; } = true;

        public Movie Movie { get; set; } = null!;
        public Room Room { get; set; } = null!;
    }

}
