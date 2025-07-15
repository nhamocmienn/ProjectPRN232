namespace RapPhim1.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Biography { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
