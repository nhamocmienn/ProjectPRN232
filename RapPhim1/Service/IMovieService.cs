using RapPhim1.Models;

namespace RapPhim1.Service
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllAsync();
        Task<Movie?> GetByIdAsync(int id);
        Task AddMovieAsync(Movie movie, List<string> actorNames, List<string> directorNames, List<int> genreIds);

        Task UpdateAsync(Movie movie, List<string> actorNames, List<string> directorNames, List<int> genreIds);
        Task DeleteAsync(int id);

    }

}
