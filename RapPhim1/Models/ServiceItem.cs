namespace RapPhim1.Models
{
   
        public class ServiceItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public decimal Price { get; set; }
            public string? ImageUrl { get; set; }
            public bool IsActive { get; set; } = true;
        }
    

}
