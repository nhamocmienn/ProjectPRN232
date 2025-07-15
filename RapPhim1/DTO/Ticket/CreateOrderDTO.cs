namespace RapPhim1.DTO.Ticket
{
    public class CreateOrderDTO
    {
        public int? UserId { get; set; } // null nếu mua tại quầy
        public List<TicketDTO> Tickets { get; set; } = new();
        public List<ServiceDTO> Services { get; set; } = new();
    }
}
