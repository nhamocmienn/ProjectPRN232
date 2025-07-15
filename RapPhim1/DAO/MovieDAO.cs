using Microsoft.EntityFrameworkCore;
using RapPhim1.Data;
using RapPhim1.Models;

namespace RapPhim1.DAO
{
    public class MovieDAO
    {
        private readonly AppDbContext _context;
        public MovieDAO(AppDbContext context) => _context = context;

        public async Task<Movie?> GetByIdAsync(int id) =>
          await _context.Movies
         .Include(m => m.MovieGenres)
             .ThenInclude(mg => mg.Genre)
         .Include(m => m.MovieActors)
             .ThenInclude(ma => ma.Actor)
         .Include(m => m.MovieDirectors)
             .ThenInclude(md => md.Director)
         .FirstOrDefaultAsync(m => m.Id == id);


        public async Task AddAsync(Movie movie) => await _context.Movies.AddAsync(movie);
        public async Task<List<Movie>> GetAllAsync() => await _context.Movies.ToListAsync();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(Movie movie) => _context.Movies.Update(movie);


        public void Delete(Movie movie) => movie.IsActive = false;

        public async Task RemoveRelationsAsync(int movieId)
        {
            var actors = _context.MovieActors.Where(ma => ma.MovieId == movieId);
            var directors = _context.MovieDirectors.Where(md => md.MovieId == movieId);
            var genres = _context.MovieGenres.Where(mg => mg.MovieId == movieId);

            _context.MovieActors.RemoveRange(actors);
            _context.MovieDirectors.RemoveRange(directors);
            _context.MovieGenres.RemoveRange(genres);

            await _context.SaveChangesAsync();
        }


    }
}
