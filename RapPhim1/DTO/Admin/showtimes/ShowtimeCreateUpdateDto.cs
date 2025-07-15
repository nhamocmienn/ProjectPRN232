namespace RapPhim1.DTO.Admin.showtimes
{
    namespace RapPhim1.DTO.Admin
    {
        public class ShowtimeCreateUpdateDto
        {
            public int MovieId { get; set; }
            public int RoomId { get; set; }
            public DateTime StartTime { get; set; }
        }
    }
}
