namespace RapPhim1.DTO.Admin.showtimes
{
    public class ShowtimeDTO
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = "";
        public int RoomId { get; set; }
        public string RoomName { get; set; } = "";
        public DateTime StartTime { get; set; }

        public bool IsActive { get; set; }
    }

}
