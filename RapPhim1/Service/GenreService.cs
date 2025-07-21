using RapPhim1.DAO;
using RapPhim1.Models;

namespace RapPhim1.Service
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllAsync();
    }

    public class GenreService : IGenreService
    {
        private readonly GenreDAO _dao;
        public GenreService(GenreDAO dao) => _dao = dao;

        public async Task<List<Genre>> GetAllAsync() => await _dao.GetAllAsync();
    }
}
