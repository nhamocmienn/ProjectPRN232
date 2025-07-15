namespace RapPhim1.DTO.Admin.serviceItem
{
    public class ServiceItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
