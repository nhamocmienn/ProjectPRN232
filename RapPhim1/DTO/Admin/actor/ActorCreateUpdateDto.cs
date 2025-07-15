namespace RapPhim1.DTO.Admin.actor
{
    public class ActorCreateUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }
    }
}
