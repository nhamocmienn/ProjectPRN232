using RapPhim1.DAO;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public class MovieService : IMovieService
    {
        private readonly MovieDAO _movieDAO;
        private readonly ActorDAO _actorDAO;
        private readonly DirectorDAO _directorDAO;
        private readonly GenreDAO _genreDAO;

        public MovieService(MovieDAO movieDAO, ActorDAO actorDAO, DirectorDAO directorDAO, GenreDAO genreDAO)
        {
            _movieDAO = movieDAO;
            _actorDAO = actorDAO;
            _directorDAO = directorDAO;
            _genreDAO = genreDAO;
        }

        public async Task<List<Movie>> GetAllAsync() => await _movieDAO.GetAllAsync();

        public async Task<Movie?> GetByIdAsync(int id) => await _movieDAO.GetByIdAsync(id);

        public async Task AddMovieAsync(Movie movie, List<string> actorNames, List<string> directorNames, List<int> genreIds)
        {
            // Xử lý thể loại
            var genres = await _genreDAO.GetByIdsAsync(genreIds);
            foreach (var genre in genres)
            {
                movie.MovieGenres.Add(new MovieGenre { Genre = genre });
            }

            // Xử lý diễn viên
            foreach (var name in actorNames)
            {
                var actor = await _actorDAO.GetByNameAsync(name);
                if (actor == null)
                {
                    actor = new Actor { Name = name };
                    await _actorDAO.AddAsync(actor);
                    await _actorDAO.SaveChangesAsync();
                }
                movie.MovieActors.Add(new MovieActor { Actor = actor });
            }

            // Xử lý đạo diễn
            foreach (var name in directorNames)
            {
                var director = await _directorDAO.GetByNameAsync(name);
                if (director == null)
                {
                    director = new Director { Name = name };
                    await _directorDAO.AddAsync(director);
                    await _directorDAO.SaveChangesAsync();
                }
                movie.MovieDirectors.Add(new MovieDirector { Director = director });
            }

            await _movieDAO.AddAsync(movie);
            await _movieDAO.SaveChangesAsync();
        }

        public async Task UpdateAsync(Movie movie, List<string> actorNames, List<string> directorNames, List<int> genreIds)
        {
            // Cập nhật thông tin cơ bản
            _movieDAO.Update(movie);

            // Xoá quan hệ cũ
            await _movieDAO.RemoveRelationsAsync(movie.Id);

            // Xử lý genre
            var genres = await _genreDAO.GetByIdsAsync(genreIds);
            foreach (var genre in genres)
            {
                movie.MovieGenres.Add(new MovieGenre { Genre = genre });
            }

            // Xử lý actor
            foreach (var name in actorNames.Distinct())
            {
                var actor = await _actorDAO.GetByNameAsync(name);
                if (actor == null)
                {
                    actor = new Actor { Name = name };
                    await _actorDAO.AddAsync(actor);
                    await _actorDAO.SaveChangesAsync(); // cần ID
                }
                movie.MovieActors.Add(new MovieActor { Actor = actor });
            }

            // Xử lý director
            foreach (var name in directorNames.Distinct())
            {
                var director = await _directorDAO.GetByNameAsync(name);
                if (director == null)
                {
                    director = new Director { Name = name };
                    await _directorDAO.AddAsync(director);
                    await _directorDAO.SaveChangesAsync(); // cần ID
                }
                movie.MovieDirectors.Add(new MovieDirector { Director = director });
            }

            await _movieDAO.SaveChangesAsync();
        }



        public async Task DeleteAsync(int id)
        {
            var movie = await _movieDAO.GetByIdAsync(id);
            if (movie != null)
            {
                _movieDAO.Delete(movie);
                await _movieDAO.SaveChangesAsync();
            }
        }

    }
}
