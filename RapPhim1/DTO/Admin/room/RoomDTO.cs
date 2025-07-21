using RapPhim1.DTO.Admin.seat;

namespace RapPhim1.DTO.Admin.room
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public ICollection<SeatDTO> Seats { get; set; } = new List<SeatDTO>();
    }
}
