namespace RapPhim1.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}
