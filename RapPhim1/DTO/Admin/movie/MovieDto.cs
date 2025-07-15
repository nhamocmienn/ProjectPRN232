namespace RapPhim1.DTO.Admin.movie
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public string LandscapeImageUrl { get; set; } = null!;
        public string TrailerUrl { get; set; } = null!;
        public bool IsActive { get; set; }

        public List<string> ActorNames { get; set; } = new();
        public List<string> DirectorNames { get; set; } = new();
        public List<string> GenreNames { get; set; } = new();
    }


}
