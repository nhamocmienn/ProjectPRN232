namespace RapPhim1.DTO.Admin.Director
{
    public class DirectorCreateUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }
    }
}
