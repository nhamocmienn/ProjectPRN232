namespace RapPhim1.DTO.Admin.seat
{
    public class SeatUpdateDTO
    {
        public int Id { get; set; }  // dùng để xác thực id
        public int RoomId { get; set; }
        public string Row { get; set; } = null!;
        public int Column { get; set; }
        public int SeatTypeId { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
