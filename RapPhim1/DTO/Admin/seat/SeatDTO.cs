namespace RapPhim1.DTO.Admin.seat
{
    public class SeatDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Row { get; set; } = null!;
        public int Column { get; set; }
        public int SeatTypeId { get; set; }
        public string SeatTypeName { get; set; } = null!;
        public decimal ExtraFee { get; set; }
    }
}
