namespace RapPhim1.DTO.Admin.seat
{
    public class SeatCreateDTO
    {
        public int RoomId { get; set; }
        public string Row { get; set; } = null!;
        public int Column { get; set; }
        public int SeatTypeId { get; set; }
    }
}
