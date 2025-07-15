namespace RapPhim1.DTO.Admin.movie
{
    public class MovieUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public string LandscapeImageUrl { get; set; } = null!;
        public string TrailerUrl { get; set; } = null!;
        public List<string> ActorNames { get; set; } = new();
        public List<string> DirectorNames { get; set; } = new();
        public List<int> GenreIds { get; set; } = new();
        public bool IsActive { get; set; }
    }
}
