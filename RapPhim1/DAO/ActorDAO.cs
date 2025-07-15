using RapPhim1.Data;
using RapPhim1.Models;
using Microsoft.EntityFrameworkCore;


namespace RapPhim1.DAO
{
    public class ActorDAO
    {
        private readonly AppDbContext _context;
        public ActorDAO(AppDbContext context) => _context = context;

        public async Task<List<Actor>> GetAllAsync() =>
            await _context.Actors.ToListAsync();

        public async Task<Actor?> GetByIdAsync(int id) =>
            await _context.Actors.FindAsync(id);

        public async Task AddAsync(Actor actor) =>
            await _context.Actors.AddAsync(actor);

        public void Update(Actor actor) =>
            _context.Actors.Update(actor);

        public void Delete(Actor actor) =>
            actor.IsActive = false;
        public async Task<Actor?> GetByNameAsync(string name) =>
           await _context.Actors.FirstOrDefaultAsync(a => a.Name == name);
        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
