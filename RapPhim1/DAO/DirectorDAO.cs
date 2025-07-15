using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;

namespace RapPhim1.DAO
{
    public class DirectorDAO
    {
        private readonly AppDbContext _context;
        public DirectorDAO(AppDbContext context) => _context = context;

        public async Task<List<Director>> GetAllAsync() =>
            await _context.Directors.ToListAsync();

        public async Task<Director?> GetByIdAsync(int id) =>
            await _context.Directors.FindAsync(id);

        public async Task AddAsync(Director director) =>
            await _context.Directors.AddAsync(director);

        public void Update(Director director) =>
            _context.Directors.Update(director);

        public void Delete(Director director) =>
            director.IsActive = false;

        public async Task<Director?> GetByNameAsync(string name) =>
         await _context.Directors.FirstOrDefaultAsync(d => d.Name == name);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }

}
