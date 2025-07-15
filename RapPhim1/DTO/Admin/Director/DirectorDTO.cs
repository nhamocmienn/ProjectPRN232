namespace RapPhim1.DTO.Admin.Director
{
    public class DirectorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
