using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;

namespace RapPhim1.DAO
{
    public class GenreDAO
    {
        private readonly AppDbContext _context;
        public GenreDAO(AppDbContext context) => _context = context;

        public async Task<List<Genre>> GetByIdsAsync(List<int> ids) =>
            await _context.Genres.Where(g => ids.Contains(g.Id)).ToListAsync();
    }

}
