namespace RapPhim1.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; } // minutes
        public string Description { get; set; } = null!;
        public string PosterUrl { get; set; } = null!; // dọc
        public string LandscapeImageUrl { get; set; } = null!; // ngang
        public string TrailerUrl { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();


        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
