namespace RapPhim1.DTO.Admin.serviceItem
{
    public class ServiceItemCreateUpdateDTO
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
